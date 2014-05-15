using System.Windows.Controls;

namespace MicroERP.Presentation.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für CustomersControl.xaml
    /// </summary>
    public partial class CustomersControl : UserControl
    {
        public CustomersControl()
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
