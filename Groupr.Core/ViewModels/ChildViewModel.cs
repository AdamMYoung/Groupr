using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace Groupr.Core.ViewModels
{
    /// <summary>
    ///     View model representing the child of a group (applications/files/folders).
    /// </summary>
    public class ChildViewModel : ViewModelBase
    {
        /// <summary>
        ///     Name of the child.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        ///     Path to the item to launch.
        /// </summary>
        [XmlElement("Path")]
        public string Path { get; set; }

        /// <summary>
        ///     Image of the group.
        /// </summary>
        [XmlIgnore]
        public BitmapSource Image { get; set; }

        /// <summary>
        ///     Unique identifier of the child.
        /// </summary>
        [XmlElement("Uid")]
        public string Uid { get; set; } = Guid.NewGuid().ToString();

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
    }
}