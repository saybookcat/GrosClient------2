using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;

namespace Framework.Json
{
    public static class JsonConverter
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Full
        };

        public static T DeserializeObject<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return default(T);

            var result = (T)JsonConvert.DeserializeObject(jsonString, typeof(T), JsonSerializerSettings);

            return result;
        }

        public static string SerializeObject<T>(T t)
        {
            if (t == null) return string.Empty;

            var result = JsonConvert.SerializeObject(t, Formatting.Indented, JsonSerializerSettings);
            return result;
        }
    }
}
