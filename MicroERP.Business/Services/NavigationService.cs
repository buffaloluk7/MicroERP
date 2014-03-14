using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xaml;

namespace MicroERP.Business.Services
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
                var viewModel = SimpleIoc.Default.GetInstance<VVM>();

                if (viewModel is INavigationAware && argument != null)
                {
                    (viewModel as INavigationAware).OnNavigatedTo(argument);
                }

                window.DataContext = viewModel;

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
