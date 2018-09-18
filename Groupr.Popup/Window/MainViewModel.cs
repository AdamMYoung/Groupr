using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Core.ViewModels;
using Groupr.IO;

namespace Groupr.Popup.Window
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            LoadGroup();
        }

        #region Variables

        /// <summary>
        ///     Group info to load.
        /// </summary>
        public GroupViewModel Group { get; private set; }

        #endregion

        /// <summary>
        ///     Loads the group using arguments passed into the application.
        /// </summary>
        private void LoadGroup()
        {
            var args = Environment.GetCommandLineArgs();
            var dictionary = new Dictionary<string, string>();

            for (var index = 1; index < args.Length; index += 2) dictionary.Add(args[index], args[index + 1]);

            if (dictionary.TryGetValue("/GroupId", out var value))
                Group = FileManager.LoadGroups().FirstOrDefault(x => x.Uid == value);
        }

        #region Commands

        /// <summary>
        ///     Command to open the provided application.
        /// </summary>
        public RelayCommand<object> OpenApplicationCommand => new RelayCommand<object>(value =>
        {
            var child = (ChildViewModel) value;
            Process.Start(child.Path);
            RequestClose?.Invoke();
        });

        /// <summary>
        ///     Event to request the closing of the application.
        /// </summary>
        public event Action RequestClose;

        #endregion
    }
}