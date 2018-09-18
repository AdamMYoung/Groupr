using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Groupr.Core.ViewModels;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace Groupr.IO
{
    /// <summary>
    ///     Class to serialize and deserialize group objects.
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Path to the Groupr application data folder.
        /// </summary>
        private static readonly string GroupsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Groupr\Data\");

        /// <summary>
        /// Path to the saved groups file.
        /// </summary>
        private static readonly string GroupsFile = Path.Combine(GroupsPath, "SavedGroups.xml");

        /// <summary>
        /// Initializes directories used by the FileManager.
        /// </summary>
        static FileManager()
        {
            if (!Directory.Exists(GroupsPath))
                Directory.CreateDirectory(GroupsPath);
        }

        /// <summary>
        ///     Loads all groups saved to the disk, if available.
        /// </summary>
        /// <returns>An ICollection of GroupViewModel objects.</returns>
        public static ICollection<GroupViewModel> LoadGroups()
        {
            if (!File.Exists(GroupsFile))
                return new List<GroupViewModel>();

            var reader = new XmlSerializer(typeof(List<GroupViewModel>));
            using (var file = new StreamReader(GroupsFile))
            {
                return (ICollection<GroupViewModel>) reader.Deserialize(file);
            }
        }

        /// <summary>
        ///     Saves all passed groups to the disk.
        /// </summary>
        /// <param name="groups">Collection of groups to save to disk.</param>
        public static void SaveGroups(List<GroupViewModel> groups)
        {
            if (groups.Count == 0)
                return;

            var writer = new XmlSerializer(typeof(List<GroupViewModel>));
            var file = File.Create(GroupsFile);

            writer.Serialize(file, groups);
            file.Close();
        }

        /// <summary>
        /// Returns the created shortcut for the provided group, or creates one if it doesn't exist.
        /// </summary>
        /// <param name="group">Group to create a shortcut from.</param>
        internal static string GetGroupShortcut(GroupViewModel group)
        {
            var appDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var folderPath = Path.Combine(appDirectory, @"Groupr\Shortcuts\");
            var shortcutPath = Path.Combine(folderPath, group.Name + " " + DateTime.Now.ToString($"yyyy-MM-dd") + ".lnk");

            Directory.CreateDirectory(folderPath);
            if (!File.Exists(shortcutPath))
                CreateGroupShortcut(group, shortcutPath);

            return folderPath;
        }

        /// <summary>
        /// Creates a shortcut, using the groupId as the name and arguments to pass to the popup window.
        /// </summary>
        /// <param name="group">Group to create.</param>
        /// <param name="shortcutPath">Location to create the shortcut.></param>
        private static void CreateGroupShortcut(GroupViewModel group, string shortcutPath)
        {   
            var shortcutAppPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory,
                @"..\..\..\Groupr.Popup\bin\Debug\Groupr.Popup.exe"));

            var wsh = new WshShell();
            if (wsh.CreateShortcut(shortcutPath) is IWshShortcut shortcut)
            {
                shortcut.TargetPath = shortcutAppPath;
                shortcut.Arguments = "/GroupId " + group.Uid;

                shortcut.Save();
            }
        }
    }
}