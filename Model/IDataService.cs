namespace Model
{
    internal interface IDataService
    {
        void Save<T>(List<T> storage);
        List<T> Load<T>();
    }
}