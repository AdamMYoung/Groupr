using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groupr.Core.ViewModels;

namespace Groupr.IO
{
    /// <summary>
    /// Class to manage taskbar interaction.
    /// </summary>
    public static class TaskbarManager
    {
        /// <summary>
        /// Pins the passed group to the taskbar, if possible.
        /// </summary>
        /// <param name="group">Group to pin to the taskbar.</param>
        public static void PinGroup(GroupViewModel group)
        {
            var shortcutPath = FileManager.GetGroupShortcut(group);
        }

        /// <summary>
        /// Unpins the passed group from the taskbar, if possible.
        /// </summary>
        /// <param name="group">Group to unpin from the taskbar.</param>
        public static void UnpinGroup(GroupViewModel group)
        {
            var shortcutPath = FileManager.GetGroupShortcut(group);
        }
    }
}
