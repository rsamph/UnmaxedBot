namespace UnmaxedBot.Core.Data
{
    public interface IObjectStore
    {
        void SaveObject(object obj, string key);
        T LoadObject<T>(string key);
        bool KeyExists(string key);
    }
}
