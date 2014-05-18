using System.Collections.Generic;
using System.Reflection;

namespace FossLock.Model.Base.SharpArchitecture
{
    /// <summary>
    ///     This serves as a base interface for <see cref="EntityWithTypedId{TId}" />. 
    /// </summary>
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; set; }

        IEnumerable<PropertyInfo> GetSignatureProperties();

        bool IsTransient();
    }
}
