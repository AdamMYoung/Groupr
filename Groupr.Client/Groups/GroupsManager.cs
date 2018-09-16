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
        /// <param name="callback">Callback for group interaction.</param>
        public GroupsManager(ICollection<GroupViewModel> groups, IGroupsListener callback)
        {
            Groups = new ObservableCollection<GroupViewModel>(groups);
            Callback = callback;

            Groups.CollectionChanged += (sender, args) =>
            {
                foreach (var groupViewModel in Groups)
                {
                    groupViewModel.PropertyChanged += GroupValueChanged;
                    groupViewModel.Children.CollectionChanged += GroupValueChanged;
                }
            };
        }

        /// <summary>
        ///     Called when any element of a group view model changes, updating the XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupValueChanged(object sender, object e)
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
                Callback.GroupSelected(_selectedGroup);
            }
            get => _selectedGroup;
        }

        private GroupViewModel _selectedGroup;

        /// <summary>
        ///     Callback used for group interaction.
        /// </summary>
        private IGroupsListener Callback { get; }

        #endregion

        #region Commands

        /// <summary>
        ///     Command to open a group creation dialog.
        /// </summary>
        public RelayCommand CreateGroupCommand => new RelayCommand(async () =>
        {
            var viewModel = new GroupViewModel();
            var view = new CreateGroupDialog
            {
                DataContext = viewModel
            };

            var result = await DialogHost.Show(view, "MainWindow", null, null);
            if ((bool?) result == true)
                Groups.Add(viewModel);
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