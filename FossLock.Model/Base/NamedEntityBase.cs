namespace FossLock.Model.Base
{
    public abstract class NamedEntityBase : EntityBase, INamedEntityBase
    {
        public string Name { get; set; }
    }
}