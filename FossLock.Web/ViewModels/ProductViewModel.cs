using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels
{
    /// <summary>
    ///     Represents a Product object in a way that can be properly
    ///     handled by a Razor template.
    /// </summary>
    public class ProductViewModel : IFossLockViewModel
    {
        /// <summary>
        ///     Initializes the ProductViewModel.
        ///     Any reference types, specifically lists, that require instantiation,
        ///     are done so by the constructor (so no properties should ever return null).
        /// </summary>
        public ProductViewModel()
        {
            // initialize our lists
            SelectedDefaultLockProperties = new List<string>();
            PermittedActivationTypes = new List<string>();
            Versions = new List<ProductVersion>();

            // the following 4 properties are used in the Razor templates to display
            // all the possible choices.  Currently all four are based on enums (but not
            // all the enum values are displayed).
            AllLockProperties = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(LockPropertyType)))
                .Where(e => (LockPropertyType)e != LockPropertyType.None)
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(LockPropertyType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            AllActivationTypes = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(ActivationType)))
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
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(ActivationType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            AllVersioningStyles = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(VersioningStyle)))
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(VersioningStyle), e),
                    Value = e.ToString()
                }), "Value", "Text");

            AllLeewayTypes = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(VersionLeewayType)))
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(VersionLeewayType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            // set our default values for other fields
            Name = string.Empty;
            ReleaseDate = DateTime.Now;
            Notes = string.Empty;
            VersioningStyle = ((int)Core.VersioningStyle.DotNet).ToString();
        }

        #region Basics

        /// <summary>
        ///     Holds the database primary key.  Shouldn't be directly displayed
        ///     or modified by the user.
        /// </summary>
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Versioning Style")]
        public string VersioningStyle { get; set; }

        #endregion Basics

        #region License/Security

        [Display(Name = "Default required hardware identifiers.")]
        public IList<string> SelectedDefaultLockProperties { get; set; }

        [Display(Name = "Licensing fails on null hardware identifier.")]
        public bool FailOnNullHardwareIdentifier { get; set; }

        [Display(Name = "Allowed Activation Modes")]
        public IList<string> PermittedActivationTypes { get; set; }

        [Required]
        [Display(Name = "Version Leeway")]
        public string VersionLeeway { get; set; }

        #endregion License/Security

        #region Children

        public IEnumerable<ProductVersion> Versions { get; set; }

        #endregion Children

        #region Selection Lists

        /*
         * Note:
         * The following lists are read-only from the outside world.
         * They are used to display all the possible chocies for a particular property
         * to the Razor engine.
         */

        public SelectList AllLockProperties { get; private set; }

        public SelectList AllActivationTypes { get; private set; }

        public SelectList AllVersioningStyles { get; private set; }

        public SelectList AllLeewayTypes { get; private set; }

        #endregion Selection Lists
    }
}