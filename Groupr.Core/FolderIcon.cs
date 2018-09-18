using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Groupr.IO
{
    /// <summary>
    /// Class representing a folder icon.
    /// </summary>
    class FolderIcon
    {
        /// <summary>
        /// Image to display.
        /// </summary>
        public BitmapImage Image { get; set; }

        /// <summary>
        /// Path to the image.
        /// </summary>
        public string ImagePath { get; set; }
    }
}
