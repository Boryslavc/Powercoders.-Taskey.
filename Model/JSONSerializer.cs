using System.Text.Json;
using System.Text.Json;

namespace Model
{
    internal class JSONSerializer: ISerializer
    {

        public string Serialize<T>(T obj) =>
            JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        

        public T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
        
    }
}
