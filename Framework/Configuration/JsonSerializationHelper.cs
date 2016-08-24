using System;
using Framework.Json;

namespace Framework.Configuration
{
    /// <summary>
    /// Json序列化对象到文件帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class JsonSerializationHelper<T> : FileStreamSerializationHelper<T>
    {
        public override void Serialize(System.IO.Stream stream, T data)
        {
            try
            {
                string json = JsonConverter.SerializeObject(data);
                using (System.IO.TextWriter writer = new System.IO.StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Serialize Failed:" + ex.Message);
                throw;
            }
        }

        public override T Deserialize(System.IO.Stream stream)
        {
            T data;
            try
            {
                using (System.IO.TextReader reader = new System.IO.StreamReader(stream))
                {
                    data = JsonConverter.DeserializeObject<T>(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Deserialize Failed:" + ex.Message);

                throw;
            }
            return data;
        }
    }
}
