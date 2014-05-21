namespace FossLock.Model.Base
{
    internal interface INamedEntityBase : IEntityBase
    {
        string Name { get; set; }
    }
}