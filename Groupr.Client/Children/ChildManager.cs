using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Groupr.Client.Children.New;
using Groupr.Client.Util;
using Groupr.Client.Util.Confirmation;
using Groupr.Core.ViewModels;
using MaterialDesignThemes.Wpf;

namespace Groupr.Client.Children
{
    /// <summary>
    ///     Manages the assigned children of a group.
    /// </summary>
    public class ChildManager : ViewModelBase
    {
        /// <summary>
        ///     Instantiates a new ChildManager.
        /// </summary>
        /// <param name="group">Group to manage.</param>
        /// <param name="childCallback">Callback to register child changes.</param>
        public ChildManager(GroupViewModel group, IChildListener childCallback)
        {
            Group = group;
            ChildCallback = childCallback;

            if (group != null)
                Group.Children.CollectionChanged += (sender, args) => ChildCallback.ChildValueChanged();
        }

        #region Variables

        /// <summary>
        ///     Currently selected group.
        /// </summary>
        public GroupViewModel Group { get; }

        /// <summary>
        ///     Callback to notify when values change.
        /// </summary>
        private IChildListener ChildCallback { get; }

        /// <summary>
        ///     Currently selected child of the group.
        /// </summary>
        public ChildViewModel SelectedChild { get; set; }

        #endregion

        #region Commands

        /// <summary>
        ///     Creates a new child entry into the current group.
        /// </summary>
        public RelayCommand CreateEntryCommand => new RelayCommand(async () =>
        {
            var viewModel = new CreateChildDialogViewModel();
            var view = new CreateChildDialog
            {
                DataContext = viewModel
            };

            var result = await DialogHost.Show(view, "MainWindow", null, null);
            if ((bool?) result == true) Group.Children.Add(viewModel.BuildChildViewModel());
        });

        /// <summary>
        ///     Deletes the selected child entry from the current group.
        /// </summary>
        public RelayCommand DeleteEntryCommand => new RelayCommand(async () =>
        {
            if (SelectedChild == null)
                return;

            var view = new ConfirmDeleteDialog();
            var result = await DialogHost.Show(view, "MainWindow", null, null);

            if ((bool?) result == true)
            {
                Group.Children.Remove(SelectedChild);
                SelectedChild = null;
            }
        });

        #endregion
    }

    /// <summary>
    ///     Listener inteface for child interaction.
    /// </summary>
    public interface IChildListener
    {
        /// <summary>
        ///     Called when a child value has been changed.
        /// </summary>
        void ChildValueChanged();
    }
}