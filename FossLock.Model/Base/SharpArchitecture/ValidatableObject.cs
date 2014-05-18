using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FossLock.Model.Base.SharpArchitecture
{
    [Serializable]
    public abstract class ValidatableObject : BaseObject
    {
        public virtual bool IsValid()
        {
            return this.ValidationResults().Count == 0;
        }

        public virtual ICollection<ValidationResult> ValidationResults()
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
            return validationResults;
        }
    }
}
