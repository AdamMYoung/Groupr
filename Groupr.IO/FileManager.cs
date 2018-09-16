﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Groupr.Core.ViewModels;

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
        /// Stored list of groups.
        /// </summary>
        private static List<GroupViewModel> Groups { get; set; }

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
        public static void SaveGroups(IList<GroupViewModel> groups)
        {
            if (groups.Count == 0)
                return;

            var writer = new XmlSerializer(typeof(List<GroupViewModel>));
            var file = File.Create(GroupsFile);

            writer.Serialize(file, groups);
            file.Close();
        }
    }
}