using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;

namespace FossLock.Web.ViewModels.Converters
{
    /// <summary>
    ///     Helper for converting between entities and their corresponding
    ///     viewmodel types.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type (typically a POCO class used by the persistence layer).
    /// </typeparam>
    /// <typeparam name="TViewModel">
    ///     The viewmodel type (typically a class rendered out by the Razor engine).
    /// </typeparam>
    public interface IEntityConverter<TEntity, TViewModel>
        where TEntity : EntityBase, new()
        where TViewModel : IFossLockViewModel, new()
    {
        /// <summary>
        ///     Takes an entity and converts it to it's corresponding viewmodel for
        ///     rendering with the Razor view engine.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance to be converted into a viewmodel.
        /// </param>
        /// <returns>
        ///     A viewmodel that represents the provided entity.
        /// </returns>
        TViewModel EntityToViewmodel(TEntity entity);

        /// <summary>
        ///     Takes both a viewmodel and it's corresponding entity instance.
        ///     Updates the properties on the entity and
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        void ViewmodelToEntity(TViewModel vm, out TEntity entity);
    }
}
