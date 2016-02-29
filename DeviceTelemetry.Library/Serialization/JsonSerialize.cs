using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeviceTelemetry.Library.Serialization
{
    public class JsonSerialize : ISerialize
    {
        /// <summary>
        /// Converts the provided object into a JSON string then a UTF8 encoded byte array
        /// </summary>
        /// <param name="objectToSerialize">Object to convert into an encoded byte array</param>
        /// <returns></returns>
        public byte[] SerializeObject(object objectToSerialize)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objectToSerialize));
        }

        /// <summary>
        /// Deserializes from a JSON string that is a UTF8 encoded byte array into the type T requested
        /// </summary>
        /// <typeparam name="T">Type to deserialize into</typeparam>
        /// <param name="bytes">Byte array to deserialize into type T</param>
        /// <returns></returns>
        public T DeserializeObject<T>(byte[] bytes) where T : class
        {
            string jsonData = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
