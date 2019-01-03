using Data;

namespace Contracts
{
    public interface ISerializer
    {
        void Serialize(BaseAssembly _object);
        BaseAssembly Deserialize();
        string GetPath();
        bool IsDeserializationPossible();
    }
}
