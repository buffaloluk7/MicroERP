namespace MicroERP.Business.Services.Interfaces
{
    public interface INavigationService
    {
        void Show<VVM>(object argument = null, bool showDialog = false);
    }
}
