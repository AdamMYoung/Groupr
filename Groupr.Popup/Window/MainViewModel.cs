using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Core.ViewModels;
using Groupr.IO;
using Groupr.Popup.Util;
using MaterialDesignThemes.Wpf;

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
        #if (DEBUG)
            Group = FileManager.LoadGroups().First();
        #endif

        #if (!DEBUG)
            var args = Environment.GetCommandLineArgs();
            var dictionary = new Dictionary<string, string>();

            for (var index = 1; index < args.Length; index += 2) dictionary.Add(args[index], args[index + 1]);

            if (dictionary.TryGetValue("/GroupId", out var value))
                Group = FileManager.LoadGroups().FirstOrDefault(x => x.Uid == value);
            #endif
        }

#region Commands

        /// <summary>
        ///     Command to open the provided application.
        /// </summary>
        public RelayCommand<object> OpenApplicationCommand => new RelayCommand<object>(async value =>
        {
            var child = (ChildViewModel) value;

            if(File.Exists(child.Path))
                Process.Start(child.Path);
            else
            {
                CanClose = false;

                var viewModel = new InvalidPathDialogViewModel(child);
                var view = new InvalidPathDialog
                {
                    DataContext = viewModel
                };

                var result = await DialogHost.Show(view, "MainWindow", null, null);
                if((bool)result == true)
                {
                    var allGroups = FileManager.LoadGroups() as List<GroupViewModel>;
                    var oldGroup = allGroups.FirstOrDefault(x => x.Children.Any(c => c.Uid == child.Uid));

                    oldGroup.Children.FirstOrDefault(x => x.Uid == child.Uid).Path = viewModel.Path;
                    FileManager.SaveGroups(allGroups);

                    Process.Start(viewModel.Path);
                }

                CanClose = true;
            }

            RequestClose?.Invoke();
        });

        /// <summary>
        ///     Event to request the closing of the application.
        /// </summary>
        public event Action RequestClose;

        /// <summary>
        /// Indicates if the window is allowed to close;
        /// </summary>
        public bool CanClose { get; private set; } = true;

#endregion
    }
}