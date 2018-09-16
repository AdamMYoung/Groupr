using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Client.Groups.New;
using Groupr.Core.ViewModels;
using MaterialDesignThemes.Wpf;

namespace Groupr.Client.Groups
{
    public class GroupsManager : ViewModelBase
    {
        /// <summary>
        ///     Instantiates a new GroupsManager.
        /// </summary>
        /// <param name="groups">Groups to manage.</param>
        public GroupsManager(ICollection<GroupViewModel> groups, IGroupsListener callback)
        {
            Groups = new ObservableCollection<GroupViewModel>(groups);
            Callback = callback;
        }

        #region Variables

        /// <summary>
        ///     Currently loaded groups.
        /// </summary>
        public ObservableCollection<GroupViewModel> Groups { get; }

        /// <summary>
        /// The currently selected group.
        /// </summary>
        public GroupViewModel SelectedGroup {
            set {
                _selectedGroup = value;
                Callback.GroupSelected(SelectedGroup);
            }
            get => _selectedGroup;
        }
        private GroupViewModel _selectedGroup;

        /// <summary>
        /// Callback used for group interaction.
        /// </summary>
        private IGroupsListener Callback { get; }

        #endregion

        #region Commands

        /// <summary>
        /// Command to open a group creation dialog.
        /// </summary>
        public  RelayCommand CreateGroupCommand => new RelayCommand(async () =>
        {
            var view = new CreateGroupDialog();

            var result = await DialogHost.Show(view, "MainWindow", null, null);
            if ((bool?)result == true)
            {

            }
        });

        /// <summary>
        /// Command to delete the selected group.
        /// </summary>
        public RelayCommand DeleteGroupCommand => new RelayCommand(() =>
        {

        });

        #endregion
    }

    /// <summary>
    /// Listener for group selection.
    /// </summary>
    public interface IGroupsListener
    {
        /// <summary>
        /// Called when a group has been selected by the user.
        /// </summary>
        /// <param name="group"></param>
        void GroupSelected(GroupViewModel group);
    }
}