using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MicroERP.Business.Common
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
