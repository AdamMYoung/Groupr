using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Core.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Groupr.Popup.Util
{
    class InvalidPathDialogViewModel : ViewModelBase
    {
        /// <summary>
        /// Instantaites a new InvalidPathDialogViewModel.
        /// </summary>
        /// <param name="child">Child to update.</param>
        public InvalidPathDialogViewModel(ChildViewModel child)
        {
            Path = child.Path;
        }

        /// <summary>
        /// Application Path.
        /// </summary>
        public string Path { get; set; }

        public string Image { get; set; }

        public string ImagePath { get;set; }

        /// <summary>
        ///     Command to open a path selection dialog.
        /// </summary>
        public RelayCommand SelectPathCommand => new RelayCommand(() =>
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
                Path = dialog.FileName;
        });
    }
}
