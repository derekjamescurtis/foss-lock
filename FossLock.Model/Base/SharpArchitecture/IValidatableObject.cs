using System;
namespace FossLock.Model.Base.SharpArchitecture
{
    public interface IValidatableObject
    {
        bool IsValid();
        System.Collections.Generic.ICollection<System.ComponentModel.DataAnnotations.ValidationResult> ValidationResults();
    }
}
