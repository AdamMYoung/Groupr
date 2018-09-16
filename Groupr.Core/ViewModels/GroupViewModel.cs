using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace Groupr.Core.ViewModels
{
    /// <summary>
    /// View model representing a group.
    /// </summary>
    public class GroupViewModel: ViewModelBase
    {
        /// <summary>
        /// Name of the group.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Image of the group.
        /// </summary>
        [XmlIgnore]
        public Bitmap Image { get; set; }

        /// <summary>
        /// Collection of all children attached to the specified group.
        /// </summary>
        [XmlElement("Children")]
        public ObservableCollection<ChildViewModel> Children { get; } =
            new ObservableCollection<ChildViewModel>();

        [XmlElement("LargeIcon")]
        public byte[] ImageSerialized
        {
            get
            {
                if (Image == null) return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    Image.Save(ms, ImageFormat.Bmp);
                    return ms.ToArray();
                }
            }
            set
            {
                if (value == null)
                {
                    Image = null;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        Image = new Bitmap(ms);
                    }
                }
            }
        }
    }
}
