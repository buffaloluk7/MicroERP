using MicroERP.Services.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MicroERP.Services.WPF.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> mapper;
        private readonly List<Window> activeWindows;

        public NavigationService(Dictionary<Type, Type> mapper)
        {
            this.mapper = mapper;
            this.activeWindows = new List<Window>();
        }

        public void Show<VVM>(object argument = null, bool showDialog = false)
        {
            Type view;

            if (this.mapper.TryGetValue(typeof(VVM), out view))
            {
                var window = (Window)Activator.CreateInstance(view);
                window.Closed += (s, e) => this.activeWindows.Remove(window);
                this.activeWindows.Add(window);

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
            Window window = activeWindows.FirstOrDefault(W => viewModel.Equals(W.DataContext));

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
