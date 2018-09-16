using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Core.ViewModels;
using Microsoft.Win32;

namespace Groupr.Client.Children.New
{
    /// <summary>
    ///     View model of the CreateChildDialog.
    /// </summary>
    internal class CreateChildDialogViewModel : ViewModelBase
    {
        /// <summary>
        ///     Name of the child.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Path of the child.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        ///     Command to open a path selection dialog.
        /// </summary>
        public RelayCommand SelectPathCommand => new RelayCommand(() =>
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true) Path = dialog.FileName;
        });

        /// <summary>
        ///     Builds a ChildViewModel from the stored data.
        /// </summary>
        /// <returns></returns>
        public ChildViewModel BuildChildViewModel()
        {
            return new ChildViewModel
            {
                Name = Name,
                Path = Path
            };
        }
    }
}