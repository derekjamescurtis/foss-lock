using System;
namespace FossLock.Model.Base
{
    interface INamedEntityBase : IEntityBase
    {
        string Name { get; set; }
    }
}
