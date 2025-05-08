
namespace Model
{
    internal class FileService : IDataService
    {
        public FileService(ISerializer serializer)
        {
            this.serializer = serializer;

            var current = Directory.GetCurrentDirectory();
            localPath = Path.Combine(current, "Saves");

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
        }

        private ISerializer serializer;
        private string localPath;
        private string fileName = "All.txt";

        public void Save<T>(List<T> tasks)
        {
            var converted = serializer.Serialize(tasks);

            var filePath = Path.Combine(localPath, fileName);
            File.WriteAllText(filePath, converted);            
        }

        public List<T> Load<T>()
        {
            var filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
                return new List<T>();

            var text = File.ReadAllText(filePath);
            return serializer.Deserialize<List<T>>(text);
        }
    }
}
