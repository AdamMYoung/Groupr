using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace Groupr.Core.ViewModels
{
    /// <summary>
    /// View model representing the child of a group (applications/files/folders).
    /// </summary>
    public class ChildViewModel: ViewModelBase
    {
        /// <summary>
        /// Name of the child.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Path to the item to launch.
        /// </summary>
        [XmlElement("Path")]
        public string Path { get; set; }

    }
}
