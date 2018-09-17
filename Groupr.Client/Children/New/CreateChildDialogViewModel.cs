using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
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
        ///     Name of the child.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Path of the child.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Image of the selected item's icon.
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
                }
            }
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
                Path = Path,
                Image =  Image
            };
        }
    }
}