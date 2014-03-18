using MicroERP.Services.Core.Navigation;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MicroERP.Services.WinRT.Navigation
{
    public class NavigationService : INavigationService
    {
        #region Properties

        private readonly Dictionary<Type, Type> mapper;
        private readonly Frame frame;

        #endregion

        #region Constructors

        public NavigationService(Dictionary<Type, Type> mapper)
        {
            this.mapper = mapper;
            this.frame = Window.Current.Content as Frame;
            this.frame.Navigated += frame_Navigated;
        }

        #endregion

        public void Show<TDestinationViewModel>(object argument = null, bool showDialog = false)
        {
            Type viewType;
            if (this.mapper.TryGetValue(typeof(TDestinationViewModel), out viewType))
            {
                frame.Navigate(viewType, argument);
            }
        }

        private void frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var navigationContent = e.Content as Page;
            var navigationAwareViewModel = navigationContent.DataContext as INavigationAware;

            if (navigationAwareViewModel != null)
            {
                navigationContent.Loaded += (s, r) => navigationAwareViewModel.OnNavigatedTo(e.Parameter);
            }
        }

        public void Close(object viewModel, string messageBoxMessage = null)
        {
        }
    }
}