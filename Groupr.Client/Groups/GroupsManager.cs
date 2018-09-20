using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Client.Groups.New;
using Groupr.Client.Util.Confirmation;
using Groupr.Core.ViewModels;
using Groupr.IO;
using MaterialDesignThemes.Wpf;

namespace Groupr.Client.Groups
{
    public class GroupsManager : ViewModelBase
    {
        /// <summary>
        ///     Instantiates a new GroupsManager.
        /// </summary>
        /// <param name="groups">Groups to manage.</param>
        /// <param name="groupsCallback">callback for group interaction.</param>
        public GroupsManager(ICollection<GroupViewModel> groups, IGroupsListener groupsCallback)
        {
            Groups = new ObservableCollection<GroupViewModel>(groups);
            GroupsCallback = groupsCallback;

            Groups.CollectionChanged += (sender, args) =>
            {
                SaveCurrentGroups();
                foreach (var groupViewModel in Groups)
                {
                    groupViewModel.PropertyChanged += (o, eventArgs) => SaveCurrentGroups();
                    groupViewModel.Children.CollectionChanged += (o, eventArgs) => SaveCurrentGroups();
                }
            };
        }

        /// <summary>
        ///     Saves the current group list to disk.
        /// </summary>
        public void SaveCurrentGroups()
        {
            FileManager.SaveGroups(new List<GroupViewModel>(Groups));
        }

        #region Variables

        /// <summary>
        ///     Currently loaded groups.
        /// </summary>
        public ObservableCollection<GroupViewModel> Groups { get; }

        /// <summary>
        ///     The currently selected group.
        /// </summary>
        public GroupViewModel SelectedGroup
        {
            set
            {
                _selectedGroup = value;
                GroupsCallback.GroupSelected(_selectedGroup);
            }
            get => _selectedGroup;
        }

        private GroupViewModel _selectedGroup;

        /// <summary>
        ///     GroupsCallback used for group interaction.
        /// </summary>
        private IGroupsListener GroupsCallback { get; }

        #endregion

        #region Commands

        /// <summary>
        ///     Command to open a group creation dialog.
        /// </summary>
        public RelayCommand CreateGroupCommand => new RelayCommand(async () =>
        {
            var viewModel = new CreateGroupDialogViewModel();
            var view = new CreateGroupDialog
            {
                DataContext = viewModel
            };

            var result = await DialogHost.Show(view, "MainWindow", null, null);
            if ((bool?) result == true)
                Groups.Add(viewModel.BuildGroupViewModel());
        });

        /// <summary>
        ///     Command to delete the selected group.
        /// </summary>
        public RelayCommand DeleteGroupCommand => new RelayCommand(async () =>
        {
            if (SelectedGroup == null)
                return;

            var view = new ConfirmDeleteDialog();
            var result = await DialogHost.Show(view, "MainWindow", null, null);

            if ((bool?) result == true)
            {
                Groups.Remove(SelectedGroup);
                SelectedGroup = null;
            }
        });

        /// <summary>
        ///     Command to pin the provided group to the taskbar.
        /// </summary>
        public RelayCommand<object> PinGroupCommand => new RelayCommand<object>(value =>
        {
            var group = value as GroupViewModel;
            TaskbarManager.PinGroup(group);
        });

        /// <summary>
        ///     Command to edit the provided group.
        /// </summary>
        public RelayCommand<object> EditGroupCommand => new RelayCommand<object>(async value =>
        {
            var group = value as GroupViewModel;

            var viewModel = new CreateGroupDialogViewModel(group);
            var view = new CreateGroupDialog
            {
                DataContext = viewModel
            };

            var result = await DialogHost.Show(view, "MainWindow", null, null);
            if ((bool?) result == true)
            {
                var index = Groups.IndexOf(group);
                Groups.Remove(group);
                Groups.Insert(index, viewModel.BuildGroupViewModel());
            }
        });

        #endregion
    }

    /// <summary>
    ///     Listener for group selection.
    /// </summary>
    public interface IGroupsListener
    {
        /// <summary>
        ///     Called when a group has been selected by the user.
        /// </summary>
        /// <param name="group"></param>
        void GroupSelected(GroupViewModel group);
    }
}