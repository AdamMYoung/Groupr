using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Groupr.Core.ViewModels;
using Groupr.IO;

namespace Groupr.Client.Groups.New
{
    internal class CreateGroupDialogViewModel : ViewModelBase
    {
        /// <summary>
        ///     Instantaites a new CreateGroupDialogViewModel.
        /// </summary>
        public CreateGroupDialogViewModel()
        {
            Images = new ObservableCollection<FolderIcon>(FileManager.LoadFolders());
            SelectedImage = Images[0];
        }

        /// <summary>
        ///     Instantaites a new CreateGroupDialogViewModel used to edit the passed group.
        /// </summary>
        public CreateGroupDialogViewModel(GroupViewModel group) : this()
        {
            WindowTitle = "Edit Group";
            Uid = group.Uid;
            Name = group.Name;
        }

        /// <summary>
        /// Title of the window.
        /// </summary>
        public string WindowTitle { get; } = "Add Group";

        /// <summary>
        /// Uid of the group.
        /// </summary>
        private string Uid { get; }
    
        /// <summary>
        ///     Name of the group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Currently selected image.
        /// </summary>
        public FolderIcon SelectedImage { get; set; }

        /// <summary>
        ///     Images to display to the user.
        /// </summary>
        public ObservableCollection<FolderIcon> Images { get; }

        /// <summary>
        ///     Builds a ChildViewModel from the stored data.
        /// </summary>
        /// <returns></returns>
        public GroupViewModel BuildGroupViewModel()
        {
            var group = new GroupViewModel
            {
                Name = Name,
                Image = SelectedImage.Image,
                ImageLocation = SelectedImage.ImagePath
            };

            if (Uid != null)
                group.Uid = Uid;

            return group;
        }
    }
}