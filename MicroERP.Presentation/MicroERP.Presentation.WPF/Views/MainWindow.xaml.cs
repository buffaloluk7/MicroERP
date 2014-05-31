using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace MicroERP.Presentation.Views
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenContextMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.ContextMenu.IsEnabled = true;
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
        }
    }
}