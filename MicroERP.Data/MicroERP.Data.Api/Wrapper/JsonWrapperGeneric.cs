using System.Runtime.Serialization;

namespace MicroERP.Data.Api.Wrapper
{
    [DataContract]
    public sealed class JsonWrapperGeneric<T>
    {
        #region Properties

        [DataMember(Name = "list")]
        public T List
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public JsonWrapperGeneric(T list)
        {
            this.List = list;
        }

        #endregion
    }
}
