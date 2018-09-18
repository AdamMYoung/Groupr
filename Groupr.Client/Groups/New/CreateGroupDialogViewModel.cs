using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Groupr.Core;
using Groupr.Core.ViewModels;
using Groupr.IO;

namespace Groupr.Client.Groups.New
{
    class CreateGroupDialogViewModel : ViewModelBase
    {
        /// <summary>
        /// Instantaites a new CreateGroupDialogViewModel.
        /// </summary>
        public CreateGroupDialogViewModel()
        {
            Images = new ObservableCollection<FolderIcon>(FileManager.LoadFolders());
            SelectedImage = Images[0];
        }

        /// <summary>
        /// Name of the group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Currently selected image.
        /// </summary>
        public FolderIcon SelectedImage { get; set; }

        /// <summary>
        /// Images to display to the user.
        /// </summary>
        public ObservableCollection<FolderIcon> Images { get; }

        /// <summary>
        ///     Builds a ChildViewModel from the stored data.
        /// </summary>
        /// <returns></returns>
        public GroupViewModel BuildGroupViewModel()
        {
            return new GroupViewModel
            {
                Name = Name,
                Image = SelectedImage.Image,
                ImageLocation = SelectedImage.ImagePath
            };
        }
    }
}
