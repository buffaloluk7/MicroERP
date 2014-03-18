using MicroERP.Services.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MicroERP.Services.WPF.Navigation
{
    public class NavigationService : INavigationService
    {
        public static readonly Dictionary<Type, Type> Mapper = new Dictionary<Type, Type>();

        public void Show<VVM>(object argument = null, bool showDialog = false)
        {
            Type view;

            if (NavigationService.Mapper.TryGetValue(typeof(VVM), out view))
            {
                var window = (Window)Activator.CreateInstance(view);

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
    }
}
