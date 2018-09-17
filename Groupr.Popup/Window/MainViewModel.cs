using GalaSoft.MvvmLight;
using Groupr.Core.ViewModels;
using Groupr.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Groupr.Popup.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Group info to load.
        /// </summary>
        private GroupViewModel Group { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            LoadGroup();
        }

        /// <summary>
        /// Loads the group from arguments passed in.
        /// </summary>
        private void LoadGroup()
        {
            string[] args = Environment.GetCommandLineArgs();
            var dictionary = new Dictionary<string, string>();

            for (int index = 1; index < args.Length; index += 2)
            {
                dictionary.Add(args[index], args[index + 1]);
            }

            if (dictionary.TryGetValue("/GroupId", out string value))
            {
                Group = FileManager.LoadGroups().FirstOrDefault(x => x.Uid == value);
            }          
        }
    }
}