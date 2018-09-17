using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Groupr.Client.Children;
using Groupr.Client.Groups;
using Groupr.Core.ViewModels;
using Groupr.IO;

namespace Groupr.Client.Window
{
    /// <summary>
    ///     Main view model for the class, containing groups and detail objects to bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase, IGroupsListener, IChildListener
    {
        /// <summary>
        ///     Instantiates a new MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            GroupsManager = new GroupsManager(FileManager.LoadGroups(), this);
        }

        /// <summary>
        ///     Manager for groups loaded into the application.
        /// </summary>
        public GroupsManager GroupsManager { get; }

        /// <summary>
        ///     Manager for children of a specific group.
        /// </summary>
        public ChildManager ChildManager { get; private set; }

        /// <summary>
        ///     Called when the children of a group has been changed, to commit the change to disk.
        /// </summary>
        public void ChildValueChanged() => GroupsManager.SaveCurrentGroups();

        /// <summary>
        ///     Called when a group has been selected by the user.
        /// </summary>
        /// <param name="group">Group that was selected.</param>
        public void GroupSelected(GroupViewModel group) => ChildManager = new ChildManager(group, this);

    }
}