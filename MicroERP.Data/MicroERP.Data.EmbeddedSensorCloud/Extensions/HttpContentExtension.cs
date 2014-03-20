using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroERP.Data.EmbeddedSensorCloud.Extensions
{
    public static class HttpContentExtension
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent content)
        {
            using (Stream rStream = await content.ReadAsStreamAsync())
            using (StreamReader sReader = new StreamReader(rStream))
            using (JsonReader jReader = new JsonTextReader(sReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jReader);
            }
        }
    }
}
