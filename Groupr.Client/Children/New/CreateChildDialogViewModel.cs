using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
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
        /// Instantiates a new CreateChildDialogViewModel.
        /// </summary>
        public CreateChildDialogViewModel() { }

        /// <summary>
        /// Instantiates a new CreateChildDialogViewModel used to edit the passed child.
        /// </summary>
        /// <param name="child"></param>
        public CreateChildDialogViewModel(ChildViewModel child)
        {
            WindowTitle = "Edit Application";
            Uid = child.Uid;
            Name = child.Name;
            Path = child.Path;
            Image = child.Image;
        }

        /// <summary>
        /// Title of the window.
        /// </summary>
        public string WindowTitle { get; } = "Add Applicaton";

        /// <summary>
        /// Uid of the child.
        /// </summary>
        private string Uid { get; }

        /// <summary>
        ///     Name of the child.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Path of the child.
        /// </summary>
        public string Path { get; private set; } = "C:/";

        /// <summary>
        ///     Image of the selected item's icon.
        /// </summary>
        public BitmapSource Image { get; private set; }

        /// <summary>
        ///     Command to open a path selection dialog.
        /// </summary>
        public RelayCommand SelectPathCommand => new RelayCommand(() =>
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;

                var icon = Icon.ExtractAssociatedIcon(Path);
                if (icon != null)
                {
                    var bitmap = icon.ToBitmap();
                    var hBitmap = bitmap.GetHbitmap();

                    Image = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,
                        Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                    if (Name == null)
                        Name = dialog.SafeFileName;
                }
            }
        });

        /// <summary>
        ///     Builds a ChildViewModel from the stored data.
        /// </summary>
        /// <returns></returns>
        public ChildViewModel BuildChildViewModel()
        {
            var child =  new ChildViewModel
            {
                Name = Name,
                Path = Path,
                Image = Image
            };

            if (Uid != null)
                child.Uid = Uid;

            return child;
        }
    }
}