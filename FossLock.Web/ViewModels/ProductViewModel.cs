using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FossLock.Core;

namespace FossLock.Web.ViewModels
{
    /// <summary>
    ///     Represents a Product object in a way that can be properly
    ///     handled by a Razor template.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        ///     Initializes the ProductViewModel.
        ///     Any reference types, specifically lists, that require instantiation,
        ///     are done so by the constructor (so no properties should ever return null).
        /// </summary>
        public ProductViewModel()
        {
            // initialize our lists
            SelectedDefaultLockProperties = new List<LockPropertyType>();
            PermittedActivationTypes = new List<ActivationType>();

            // the following 4 properties are used in the Razor templates to display
            // all the possible choices.  Currently all four are based on enums (but not
            // all the enum values are displayed).
            AllLockProperties = ((IEnumerable<int>)Enum.GetValues(typeof(LockPropertyType)))
                .Where(e => (LockPropertyType)e != LockPropertyType.None)
                .Select(e => new SelectListItem
                {
                    Text = Enum.GetName(typeof(LockPropertyType), e),
                    Value = e.ToString()
                })
                .ToList();

            AllActivationTypes = ((IEnumerable<int>)Enum.GetValues(typeof(ActivationType)))
                .Where(e =>
                {
                    // the following types are not currently permitted and shouldn't be shown
                    if ((ActivationType)e != ActivationType.None &&
                        (ActivationType)e != ActivationType.Phone &&
                        (ActivationType)e != ActivationType.SMS)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                })
                .Select(e => new SelectListItem
                {
                    Text = Enum.GetName(typeof(ActivationType), e),
                    Value = e.ToString()
                })
                .ToList();

            AllVersioningStyles = ((IEnumerable<int>)Enum.GetValues(typeof(VersioningStyle)))
                .Select(e => new SelectListItem
                {
                    Text = Enum.GetName(typeof(VersioningStyle), e),
                    Value = e.ToString()
                })
                .ToList();

            AllLeewayTypes = ((IEnumerable<int>)Enum.GetValues(typeof(VersionLeewayType)))
                .Select(e => new SelectListItem
                {
                    Text = Enum.GetName(typeof(VersionLeewayType), e),
                    Value = e.ToString()
                })
                .ToList();

            // set our default values for other fields
            Name = string.Empty;
            ReleaseDate = DateTime.Now;
            Notes = string.Empty;
            VersioningStyle = Core.VersioningStyle.DotNet;
        }

        #region Basics

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        // TODO: format in web standard
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Versioning Style")]
        public VersioningStyle VersioningStyle { get; set; }

        #endregion Basics

        #region License/Security

        [Display(Name = "Default required hardware identifiers.")]
        public IList<LockPropertyType> SelectedDefaultLockProperties { get; set; }

        [Display(Name = "Licensing fails on null hardware identifier.")]
        public bool FailOnNullHardwareIdentifier { get; set; }

        [Display(Name = "Allowed Activation Modes")]
        public IList<ActivationType> PermittedActivationTypes { get; set; }

        [Required]
        [Display(Name = "Version Leeway")]
        public VersionLeewayType VersionLeeway { get; set; }

        #endregion License/Security

        #region Selection Lists

        /*
         * Note:
         * The following lists are read-only from the outside world.
         * They are used to display all the possible chocies for a particular property
         * to the Razor engine.
         */

        public IList<SelectListItem> AllLockProperties { get; private set; }

        public IList<SelectListItem> AllActivationTypes { get; private set; }

        public IList<SelectListItem> AllVersioningStyles { get; private set; }

        public IList<SelectListItem> AllLeewayTypes { get; private set; }

        #endregion Selection Lists
    }
}