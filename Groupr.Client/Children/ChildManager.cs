using GalaSoft.MvvmLight;
using Groupr.Core.ViewModels;

namespace Groupr.Client.GroupsDetail
{
    /// <summary>
    ///     Manages the assigned children of a group.
    /// </summary>
    public class ChildManager : ViewModelBase
    {
        /// <summary>
        ///     Instantiates a new ChildManager.
        /// </summary>
        /// <param name="group">Group to manage.</param>
        public ChildManager(GroupViewModel group)
        {
            Group = group;
        }

        /// <summary>
        ///     Currently selected group.
        /// </summary>
        public GroupViewModel Group { get; }
    }
}