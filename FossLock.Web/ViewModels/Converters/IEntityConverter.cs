using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;

namespace FossLock.Web.ViewModels.Converters
{
    public interface IEntityConverter<TEntity, TViewModel>
        where TEntity : EntityBase, new()
        where TViewModel : IFossLockViewModel, new()
    {
        TEntity ViewmodelToEntity(TViewModel vm);

        TEntity ViewmodelToEntity(TViewModel vm, TEntity entity);

        TViewModel EntityToViewmodel(TEntity entity);
    }
}
