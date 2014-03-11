using MahApps.Metro.Controls;
using System.Windows;

namespace MicroERP.Presentation
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("LOL");
        }
    }
}
