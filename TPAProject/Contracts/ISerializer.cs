using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
