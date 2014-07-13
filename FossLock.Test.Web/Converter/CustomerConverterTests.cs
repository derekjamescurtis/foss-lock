using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;
using FossLock.Model;
using FossLock.Model.Component;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Converter
{
    [TestFixture]
    internal class CustomerConverterTests
    {
        private CustomerConverter converter = null;
        private Customer fakeEntity = null;
        private CustomerViewModel fakeViewModel = null;

        /// <summary>
        ///     Instantiates new instances of converter, fakeEntity, and
        ///     fakeViewModel before each test is run.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            converter = new CustomerConverter();

            fakeEntity = new Customer
            {
                Id = NumberFaker.Number(1, Int16.MaxValue),
                Name = StringFaker.Alpha(25),
                CanLicensePreReleaseVersions = BooleanFaker.Boolean(),
                StreetAddress = new Address
                {
                    Address1 = LocationFaker.Street(),
                    Address2 = LocationFaker.Street(),
                    City = LocationFaker.City(),
                    State = StringFaker.Alpha(2),
                    PostalCode = LocationFaker.PostCode(),
                    Country = LocationFaker.Country()
                },
                BillingAddress = new Address
                {
                    Address1 = LocationFaker.Street(),
                    Address2 = LocationFaker.Street(),
                    City = LocationFaker.City(),
                    State = StringFaker.Alpha(2),
                    PostalCode = LocationFaker.PostCode(),
                    Country = LocationFaker.Country()
                },
                OfficePhone1 = PhoneFaker.Phone(),
                OfficePhone2 = PhoneFaker.Phone(),
                OfficeFax = PhoneFaker.Phone(),
                Email = InternetFaker.Email(),
                PrimaryContact = new HumanContact
                {
                    FirstName = NameFaker.FirstName(),
                    LastName = NameFaker.LastName(),
                    Email = InternetFaker.Email(),
                    Fax = PhoneFaker.Phone(),
                    Phone1 = PhoneFaker.Phone(),
                    Phone2 = PhoneFaker.Phone(),
                    Notes = TextFaker.Sentence()
                },
                Notes = TextFaker.Sentences(5)
            };

            fakeViewModel = new CustomerViewModel
            {
            };
        }
    }
}
