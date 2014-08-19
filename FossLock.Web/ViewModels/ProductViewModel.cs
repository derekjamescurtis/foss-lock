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
            Features = new List<ProductFeature>();

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
                ((IEnumerable<int>)Enum.GetValues(typeof(ActivationMethodType)))
                .Where(e =>
                {
                    // the following types are not currently permitted and shouldn't be shown
                    if ((ActivationMethodType)e != ActivationMethodType.None &&
                        (ActivationMethodType)e != ActivationMethodType.Phone &&
                        (ActivationMethodType)e != ActivationMethodType.SMS)
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
                    Text = Enum.GetName(typeof(ActivationMethodType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            AllLeewayTypes = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(VersionLeewayType)))
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(VersionLeewayType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            AllEncryptionTypes = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(EncryptionType)))
                .Select(e => new
                {
                    Text = Enum.GetName(typeof(EncryptionType), e),
                    Value = e.ToString()
                }), "Value", "Text");

            // set our default values for other fields
            Name = string.Empty;
            ReleaseDate = DateTime.Now;
            Notes = string.Empty;
        }

        /// <summary>
        ///     Holds the database primary key.  Shouldn't be directly displayed
        ///     or modified by the user.
        /// </summary
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public EncryptionType LicenseEncryptionType { get; set; }

        [AllowHtml]
        public string PublicKey { get; set; }

        [AllowHtml]
        public string PrivateKey { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Default required hardware identifiers.")]
        public IList<string> SelectedDefaultLockProperties { get; set; }

        [Display(Name = "Licensing fails on null hardware identifier.")]
        public bool FailOnNullHardwareIdentifier { get; set; }

        [Display(Name = "Allowed Activation Modes")]
        public IList<string> PermittedActivationTypes { get; set; }

        [Required]
        [Display(Name = "Version Leeway")]
        public string VersionLeeway { get; set; }

        public IEnumerable<ProductVersion> Versions { get; set; }

        public IEnumerable<ProductFeature> Features { get; set; }

        /*
         * Note:
         * The following lists are read-only from the outside world.
         * They are used to display all the possible chocies for a particular property
         * to the Razor engine.
         */

        public SelectList AllLockProperties { get; private set; }

        public SelectList AllActivationTypes { get; private set; }

        public SelectList AllLeewayTypes { get; private set; }

        public SelectList AllEncryptionTypes { get; private set; }
    }
}