using System;
using System.Windows;
using System.Windows.Forms;

namespace Groupr.Popup.Window
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        /// <summary>
        ///     Instantiates the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }


        /// <summary>
        ///     Called when the window has loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, object e)
        {
            var viewModel = new MainViewModel();
            viewModel.RequestClose += Close;
            DataContext = viewModel;

            MoveWindowToCursor();
        }

        /// <summary>
        ///     Closes the window whenever the focus is lost.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            Close();
        }

        /// <summary>
        ///     Moves the window to the cursor.
        /// </summary>
        private void MoveWindowToCursor()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X - ActualWidth / 2;
            Top = SystemParameters.WorkArea.Height - 250;
        }

        /// <summary>
        ///     Gets the X and Y position of the mouse on the screen.
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            var point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }
    }
}