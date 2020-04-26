using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using NotesKeeper.Common.Interfaces;
using System;
using System.IO;

namespace NotesKeeper.Common.Serializers
{
    public class JsonCustomSerializer<T> : ISerializer<T>
    {
        public T Deserialize(string serializedItem)
        {
            return JsonConvert.DeserializeObject<T>(serializedItem);
        }

        public string Serialize(T item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
