using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Groupr.Client.Groups;
using Groupr.Client.GroupsDetail;
using Groupr.Core.ViewModels;
using Groupr.IO;

namespace Groupr.Client.Window
{
    /// <summary>
    /// Main view model for the class, containing groups and detail objects to bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase, IGroupsListener
    {
        /// <summary>
        /// Manager for groups loaded into the application.
        /// </summary>
        public GroupsManager GroupsManager { get; }

        /// <summary>
        /// Manager for children of a specific group.
        /// </summary>
        public ChildManager ChildManager { get; private set; }

        /// <summary>
        /// Instantiates a new MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            GroupsManager = new GroupsManager(FileManager.LoadGroups(), this);
        }

        /// <summary>
        /// Called when a group has been selected by the user.
        /// </summary>
        /// <param name="group"></param>
        public void GroupSelected(GroupViewModel @group)
        {
            ChildManager = new ChildManager(group);
        }
    }
}