﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FossLock.Core;

namespace FossLock.Web.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
        }

        #region Basics

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date",
            Description = "Date the product was first avaialble for licensing (for reporting purposes only).")]
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
        [DefaultValue(true)]
        public bool FailOnNullHardwareIdentifier { get; set; }

        [Display(Name = "Allowed Activation Modes")]
        public IList<ActivationType> PermittedActivationTypes { get; set; }

        [Display(Name = "Version Leeway")]
        [Required]
        [DefaultValue(VersionLeewayType.WithinSameMajorVersion)]
        public VersionLeewayType VersionLeeway { get; set; }

        #endregion License/Security

        #region Selection Lists

        private IEnumerable<SelectListItem> _allLockProeprties = null;

        public IEnumerable<SelectListItem> AllLockProperties
        {
            get
            {
                if (_allLockProeprties == null)
                {
                    _allLockProeprties =
                        ((IEnumerable<int>)Enum.GetValues(typeof(LockPropertyType)))
                        .Where(e => (LockPropertyType)e != LockPropertyType.None)
                        .Select(e => new SelectListItem
                        {
                            Text = Enum.GetName(typeof(LockPropertyType), e),
                            Value = e.ToString()
                        });
                }
                return _allLockProeprties;
            }
        }

        private IEnumerable<SelectListItem> _allActivationTypes = null;

        public IEnumerable<SelectListItem> AllActivationTypes
        {
            get
            {
                if (_allActivationTypes == null)
                {
                    _allActivationTypes = ((IEnumerable<int>)Enum.GetValues(typeof(ActivationType)))
                        .Where(e =>
                        {
                            var canUse = false;

                            // the following types are not currently permitted and shouldn't be shown
                            if ((ActivationType)e != ActivationType.None &&
                                (ActivationType)e != ActivationType.Phone &&
                                (ActivationType)e != ActivationType.SMS)
                            {
                                canUse = true;
                            }

                            return canUse;
                        })
                        .Select(e => new SelectListItem
                        {
                            Text = Enum.GetName(typeof(ActivationType), e),
                            Value = e.ToString()
                        });
                }

                return _allActivationTypes;
            }
        }

        private IEnumerable<SelectListItem> _allVersioningStyles = null;

        public IEnumerable<SelectListItem> AllVersioningStyles
        {
            get
            {
                if (_allVersioningStyles == null)
                {
                    _allVersioningStyles =
                        ((IEnumerable<int>)Enum.GetValues(typeof(VersioningStyle)))
                        .Select(e => new SelectListItem
                        {
                            Text = Enum.GetName(typeof(VersioningStyle), e),
                            Value = e.ToString()
                        });
                }

                return _allVersioningStyles;
            }
        }

        private IEnumerable<SelectListItem> _allLeewayTypes = null;

        public IEnumerable<SelectListItem> AllLeewayTypes
        {
            get
            {
                if (_allLeewayTypes == null)
                {
                    _allLeewayTypes =
                        ((IEnumerable<int>)Enum.GetValues(typeof(VersionLeewayType)))
                        .Select(e => new SelectListItem
                        {
                            Text = Enum.GetName(typeof(VersionLeewayType), e),
                            Value = e.ToString()
                        });
                }

                return _allLeewayTypes;
            }
        }

        #endregion Selection Lists
    }
}