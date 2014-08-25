using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.Core.Util;

namespace FossLock.Web.ViewModels.Helpers
{
    public class LocaleViewModel
    {
        private static List<SelectListItem> _countriesSelectList;

        public static IEnumerable<SelectListItem> CountriesSelectList
        {
            get
            {
                if (_countriesSelectList == null)
                {
                    _countriesSelectList = new List<SelectListItem>();

                    var blankCountry = new SelectListItem { Value = "", Text = "---" };

                    var allCountries = Locale.AllCountries().Select(
                                            c => new SelectListItem
                                            {
                                                Value = c.Id,
                                                Text = c.Title
                                            });

                    _countriesSelectList.Add(blankCountry);
                    _countriesSelectList.AddRange(allCountries);
                }

                return _countriesSelectList;
            }
        }
    }
}