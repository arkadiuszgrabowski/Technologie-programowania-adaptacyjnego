using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum AbstractEnum
    {
        NotAbstract, Abstract
    }
    public enum AccessLevel
    {
        None, Public, Protected, Internal, Private
    }
    public enum SealedEnum
    {
        NotSealed, Sealed
    }
    public enum StaticEnum
    {
        NotStatic, Static
    }
    public enum TypeEnum
    {
        None, Enum, Struct, Interface, Class
    }
    public enum VirtualEnum
    {
        NotVirtual, Virtual
    }
}

