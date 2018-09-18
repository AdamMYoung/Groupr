using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace Groupr.Core.ViewModels
{
    /// <summary>
    ///     View model representing a group.
    /// </summary>
    public class GroupViewModel : ViewModelBase
    {
        #region Variables

        /// <summary>
        ///     Name of the group.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        ///     Image of the group.
        /// </summary>
        [XmlIgnore]
        public BitmapSource Image { get; set; }

        /// <summary>
        ///     Location of the image for the group.
        /// </summary>
        public string ImageLocation { get; set; }

        /// <summary>
        ///     Indicates if the group is pinned to the taskbar.
        /// </summary>
        [XmlElement("IsPinned")]
        public bool IsPinned { get; set; }

        /// <summary>
        ///     Unique identifier of the group.
        /// </summary>
        [XmlElement("Uid")]
        public string Uid { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     Collection of all children attached to the specified group.
        /// </summary>
        [XmlElement("Children")]
        public ObservableCollection<ChildViewModel> Children { get; } = new ObservableCollection<ChildViewModel>();

        /// <summary>
        ///     Serialized version of the assigned image.
        /// </summary>
        [XmlElement("LargeIcon")]
        public string ImageSerialized
        {
            get
            {
                if (Image == null)
                    return null;

                var encoder = new PngBitmapEncoder();
                var frame = BitmapFrame.Create(Image);
                encoder.Frames.Add(frame);
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
            set
            {
                if (value == null)
                    return;

                var stream = new MemoryStream(Convert.FromBase64String(value))
                {
                    Position = 0
                };

                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                Image = image;
            }
        }

        #endregion
    }
}