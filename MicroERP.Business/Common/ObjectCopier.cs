using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace MicroERP.Business.Common
{
    public static class ObjectCopier
    {
        public static T Clone<T>(this T source, object argument = null)
        {
            if (!Attribute.IsDefined(typeof(T), typeof(DataContractAttribute)))
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var jsonObject = JsonConvert.SerializeObject(source, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            var newObject = JsonConvert.DeserializeObject<T>(jsonObject, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            
            return newObject;
        }
    }
}