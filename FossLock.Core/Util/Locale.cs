using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossLock.Core.Util
{
    public class Country
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }

    public static class Locale
    {
        /// <summary>
        ///     Returns a list of Countries and their ISO 3-letter code,
        ///     ordered in alphabetical order by their Display name (not their ISO code)
        /// </summary>
        /// <returns>A list of Country objects</returns>
        public static IEnumerable<Country> AllCountries()
        {
            var countries = from ri in
                                from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                select new RegionInfo(ci.LCID)
                            group ri by ri.ThreeLetterISORegionName into g
                            select new Country
                            {
                                Id = g.Key,
                                Title = g.First().DisplayName
                            };

            return countries.OrderBy(c => c.Title);
        }
    }
}
