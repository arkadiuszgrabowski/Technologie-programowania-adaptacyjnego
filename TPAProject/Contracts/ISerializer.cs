namespace Contracts
{
    public interface ISerializer
    {
        void Serialize<T>(T _object);
        T Deserialize<T>();
        string GetPath();
        bool IsDeserializationPossible();
    }
}
