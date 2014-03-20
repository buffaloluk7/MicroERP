using MicroERP.Services.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MicroERP.Services.WPF.Navigation
{
    public class NavigationService : INavigationService
    {
        #region Properties

        private readonly Dictionary<Type, Type> mapper;
        private readonly List<Window> openedWindows;

        #endregion

        #region Constructors

        public NavigationService(Dictionary<Type, Type> mapper)
        {
            this.mapper = mapper;
            this.openedWindows = new List<Window>();
        }

        #endregion

        public void Navigate<TViewModel>(object argument = null, bool showDialog = false)
        {
            Type viewType;

            if (this.mapper.TryGetValue(typeof(TViewModel), out viewType))
            {
                Window window = (Window)Activator.CreateInstance(viewType);
                window.Closed += (s, e) => this.openedWindows.Remove(window);
                this.openedWindows.Add(window);

                if (window.DataContext is INavigationAware)
                {
                    (window.DataContext as INavigationAware).OnNavigatedTo(argument);
                }

                if (showDialog)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
        }

        public void Close(object viewModel, string messageBoxMessage = null)
        {
            Window window = this.openedWindows.FirstOrDefault(W => viewModel.Equals(W.DataContext));

            if (window == null)
            {
                return;
            }

            if (messageBoxMessage != null && MessageBox.Show(messageBoxMessage, "Bestätigen Sie Ihre Aktion", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                return;
            }
                
            window.Close();
        }
    }
}
