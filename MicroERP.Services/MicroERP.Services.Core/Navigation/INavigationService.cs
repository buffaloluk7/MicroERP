namespace MicroERP.Services.Core.Navigation
{
    public interface INavigationService
    {
        void Navigate<TViewModel>(object argument = null, bool showDialog = false);

        void Close(object viewModel, string messageBoxMessage = null);
    }
}
