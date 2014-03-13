using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xaml;

namespace MicroERP.Business.Services
{
    public class WindowService : IWindowService
    {
        public static readonly Dictionary<Type, Type> Mapper = new Dictionary<Type, Type>();

        public void Show<VVM>(bool showDialog = false)
        {
            Type view;

            if (WindowService.Mapper.TryGetValue(typeof(VVM), out view))
            {
                var window = (Window)Activator.CreateInstance(view);
                window.DataContext = SimpleIoc.Default.GetInstance<VVM>();
                
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
