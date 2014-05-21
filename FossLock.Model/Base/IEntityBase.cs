using FossLock.Model.Base.SharpArchitecture;

namespace FossLock.Model.Base
{
    public interface IEntityBase : IBaseObject, IEntityWithTypedId<int>, IValidatableObject
    {
    }
}