using System;
using System.Collections.Generic;
using System.Reflection;
namespace FossLock.Model.Base.SharpArchitecture
{
    public interface IBaseObject
    {
        bool Equals(object obj);
        int GetHashCode();
        IEnumerable<PropertyInfo> GetSignatureProperties();
        bool HasSameObjectSignatureAs(BaseObject compareTo);
    }
}
