using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;

namespace FossLock.Web.ViewModels.Converters
{
    public interface IEntityConverter<TEntity, TViewModel>
        where TEntity : EntityBase
        where TViewModel : class
    {
        TEntity ViewmodelToEntity(TViewModel vm);

        TViewModel EntityToViewmodel(TEntity entity);
    }
}
