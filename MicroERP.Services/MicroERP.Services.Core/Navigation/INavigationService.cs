﻿namespace MicroERP.Services.Core.Navigation
{
    public interface INavigationService
    {
        void Show<VVM>(object argument = null, bool showDialog = false);
    }
}