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
                Id = NumberFaker.Number(1, int.MaxValue),
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
                Id = NumberFaker.Number(1, int.MaxValue),
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
                BillingMatchesStreetAddress = BooleanFaker.Boolean(),
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
        }

        /// <summary>
        ///     Both overloads of this method should throw an exception if any of the
        ///     provided arguments are null.
        /// </summary>
        [Test]
        public void ViewmodelToEntity_NullArgument_ThrowsException()
        {
            // single parameter flavor
            var ex = Assert.Throws<ArgumentNullException>(new TestDelegate(
                () => { converter.ViewmodelToEntity(null); }));

            Assert.That(ex.ParamName, Is.EqualTo("vm"));

            // the following are all double-param flavor
            // null entity
            ex = Assert.Throws<ArgumentNullException>(new TestDelegate(
                () => { converter.ViewmodelToEntity(fakeViewModel, null); }));

            Assert.That(ex.ParamName, Is.EqualTo("entity"));

            // null viewmodel
            ex = Assert.Throws<ArgumentNullException>(new TestDelegate(
                () => { converter.ViewmodelToEntity(null, fakeEntity); }));

            Assert.That(ex.ParamName, Is.EqualTo("vm"));

            // both null!
            ex = Assert.Throws<ArgumentNullException>(new TestDelegate(
                () => { converter.ViewmodelToEntity(null, null); }));

            Assert.That(ex.ParamName, Is.EqualTo("vm").Or.EqualTo("entity"));
        }

        /// <summary>
        ///     EntityToViewmodel requires a single, non-null argument.
        /// </summary>
        [Test]
        public void EntityToViewmodel_NullArgument_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(new TestDelegate(
                () => { converter.EntityToViewmodel(null); }));

            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        /// <summary>
        ///     Make sure the entity returned from ViewmodelToEntity is the same instance
        ///     as the entity passed into the function.
        ///
        ///     NOTE: This is done simply to keep the return type consistant with the other
        ///           overload of this method to avoid confusion.
        /// </summary>
        [Test]
        public void ViewmodelToEntity_TwoArgumentOverload_ReturnsSameEntityReference()
        {
            var returnedEntity = converter.ViewmodelToEntity(fakeViewModel, fakeEntity);
            Assert.AreSame(fakeEntity, returnedEntity);
        }

        [Test]
        public void ViewmodelToEntity_ReturnValue_MatchesExpectedState()
        {
            // we're going to run basically the same assertions against the two different
            // overloads, so I'm just going to put it in a lambda
            Action<CustomerViewModel, Customer> doAssertions = (CustomerViewModel vm, Customer e) =>
            {
                Assert.That(vm.Id, Is.EqualTo(e.Id));
                Assert.That(vm.Name, Is.EqualTo(e.Name));
                Assert.That(vm.CanLicensePreReleaseVersions, Is.EqualTo(e.CanLicensePreReleaseVersions));
                Assert.That(vm.StreetAddress, Is.EqualTo(e.StreetAddress));

                // If the viewmodel has the 'BillingMatchesStreetAddress' flag,
                // billing+street address should have value equality
                if (vm.BillingMatchesStreetAddress)
                    Assert.That(e.StreetAddress, Is.EqualTo(e.BillingAddress));
                else
                    Assert.That(vm.BillingAddress, Is.EqualTo(e.BillingAddress));

                Assert.That(vm.OfficePhone1, Is.EqualTo(e.OfficePhone1));
                Assert.That(vm.OfficePhone2, Is.EqualTo(e.OfficePhone2));
                Assert.That(vm.OfficeFax, Is.EqualTo(e.OfficeFax));
                Assert.That(vm.Email, Is.EqualTo(e.Email));
                Assert.That(vm.Notes, Is.EqualTo(e.Notes));
                Assert.That(vm.PrimaryContact, Is.EqualTo(e.PrimaryContact));
            };

            var postConversionEntity = converter.ViewmodelToEntity(fakeViewModel, fakeEntity);
            doAssertions(fakeViewModel, postConversionEntity);

            postConversionEntity = converter.ViewmodelToEntity(fakeViewModel);
            doAssertions(fakeViewModel, postConversionEntity);
        }

        [Test]
        public void EntityToViewmodel_ReturnValue_MatchesExpectedState()
        {
            Assert.Inconclusive("Not Implemented");
        }
    }
}
