using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.Services.Interfaces
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object argument);
    }
}
