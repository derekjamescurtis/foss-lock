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

        [Test]
        public void ViewmodelToEntity_ReturnValue_MatchesExpectedState()
        {
            // we're going to run basically the same assertions against the two different
            // overloads, so I'm just going to put it in a lambda
            Action<CustomerViewModel, Customer> doAssertions = (CustomerViewModel vm, Customer e) =>
            {
                Assert.That(vm.Id, Is.EqualTo(e.Id));
                Assert.That(vm.Name, Is.EqualTo(e.Name));
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

            converter.ViewmodelToEntity(fakeViewModel, ref fakeEntity);
            doAssertions(fakeViewModel, fakeEntity);

            converter.ViewmodelToEntity(fakeViewModel, ref fakeEntity);
            doAssertions(fakeViewModel, fakeEntity);
        }

        [Test]
        public void EntityToViewmodel_ReturnValue_MatchesExpectedState()
        {
            var vm = converter.EntityToViewmodel(fakeEntity);

            Assert.That(vm.Id, Is.EqualTo(fakeEntity.Id));
            Assert.That(vm.Name, Is.EqualTo(fakeEntity.Name));
            // first run, billing+street should be different
            Assert.That(vm.StreetAddress, Is.EqualTo(fakeEntity.StreetAddress));
            Assert.That(vm.BillingAddress, Is.EqualTo(fakeEntity.BillingAddress));
            Assert.That(vm.BillingMatchesStreetAddress, Is.False);
            Assert.That(vm.OfficePhone1, Is.EqualTo(fakeEntity.OfficePhone1));
            Assert.That(vm.OfficePhone2, Is.EqualTo(fakeEntity.OfficePhone2));
            Assert.That(vm.OfficeFax, Is.EqualTo(fakeEntity.OfficeFax));
            Assert.That(vm.Email, Is.EqualTo(fakeEntity.Email));
            Assert.That(vm.Notes, Is.EqualTo(fakeEntity.Notes));
            Assert.That(vm.PrimaryContact, Is.EqualTo(fakeEntity.PrimaryContact));

            // now, we're going to make a shallow copy of the street address so it's equal to billing.
            fakeEntity.BillingAddress.Address1 = fakeEntity.StreetAddress.Address1;
            fakeEntity.BillingAddress.Address2 = fakeEntity.StreetAddress.Address2;
            fakeEntity.BillingAddress.City = fakeEntity.StreetAddress.City;
            fakeEntity.BillingAddress.State = fakeEntity.StreetAddress.State;
            fakeEntity.BillingAddress.PostalCode = fakeEntity.StreetAddress.PostalCode;
            fakeEntity.BillingAddress.Country = fakeEntity.StreetAddress.Country;

            vm = converter.EntityToViewmodel(fakeEntity);

            Assert.That(vm.StreetAddress, Is.EqualTo(vm.BillingAddress));
            Assert.That(vm.BillingMatchesStreetAddress, Is.True);
        }
    }
}
