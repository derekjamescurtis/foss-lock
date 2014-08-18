using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Faker;
using FossLock.Core;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Converter
{
    /// <summary>
    ///     Unit tests for FossLock.Web.ViewModels.Converters.ProductEntityConverter
    /// </summary>
    [TestFixture]
    internal class ProductConverterTests
    {
        private ProductConverter converter = null;
        private Product fakeProduct = null;
        private ProductViewModel fakeViewmodel = null;

        /// <summary>
        ///     Reinitializes converter and fakeProduct before each test.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            converter = new ProductConverter();

            fakeProduct = new Product
            {
                Id = NumberFaker.Number(1, int.MaxValue),
                Name = StringFaker.Alpha(20),
                ReleaseDate = DateTime.Now,
                Notes = TextFaker.Sentences(5),
                LicenseEncryptionType = EncryptionType.RSA_4096,
                PrivateKey = StringFaker.AlphaNumeric(50),
                PublicKey = StringFaker.AlphaNumeric(50),
                DefaultLockProperties = LockPropertyType.CPU | LockPropertyType.BIOS,
                FailOnNullHardwareIdentifier = BooleanFaker.Boolean(),
                PermittedActivationTypes = ActivationMethodType.Manual | ActivationMethodType.Email,
                PermittedExpirationTypes = LicenseType.Service,
                VersionLeeway = VersionLeewayType.Strict,
            };

            fakeViewmodel = new ProductViewModel
            {
                Id = NumberFaker.Number(1, int.MaxValue),
                Name = StringFaker.Alpha(20),
                ReleaseDate = DateTime.Now,
                Notes = TextFaker.Sentences(5),
                SelectedDefaultLockProperties = new List<string> { ((int)LockPropertyType.CPU).ToString(), ((int)LockPropertyType.BIOS).ToString() },
                FailOnNullHardwareIdentifier = BooleanFaker.Boolean(),
                PermittedActivationTypes = new List<string> { ((int)ActivationMethodType.Manual).ToString(), ((int)ActivationMethodType.Email).ToString() },
                // randomly selects a versionleewaytype
                VersionLeeway = ((int)VersionLeewayType.Strict).ToString()
            };
        }

        [Test]
        public void EntityToViewmodel_ValidEntity_ReturnsExpectedViewmodel()
        {
            var vm = converter.EntityToViewmodel(fakeProduct);

            Assert.AreEqual(fakeProduct.Id, vm.Id);
            Assert.AreEqual(fakeProduct.Name, vm.Name);
            Assert.AreEqual(fakeProduct.ReleaseDate, vm.ReleaseDate);
            Assert.AreEqual(fakeProduct.Notes, vm.Notes);

            Assert.AreEqual(fakeProduct.FailOnNullHardwareIdentifier, vm.FailOnNullHardwareIdentifier);
            Assert.AreEqual(fakeProduct.VersionLeeway, Enum.Parse(typeof(VersionLeewayType), vm.VersionLeeway));

            // the following two asserts are a little more tricky.
            // we have to convert the list of enums (stored in the view model)
            // to a single value using bitwise math.
            var actualLockProperties = LockPropertyType.None;
            foreach (var lock_property in vm.SelectedDefaultLockProperties)
                actualLockProperties |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_property);

            Assert.AreEqual(fakeProduct.DefaultLockProperties, actualLockProperties);

            var actualActivationTypes = ActivationMethodType.None;
            foreach (var activation_type in vm.PermittedActivationTypes)
                actualActivationTypes |= (ActivationMethodType)Enum.Parse(typeof(ActivationMethodType), activation_type);

            Assert.AreEqual(fakeProduct.PermittedActivationTypes, actualActivationTypes);
        }

        [Test]
        public void ViewmodelToEntity_ValidViewmodel_ReturnsExpectedResult()
        {
            converter.ViewmodelToEntity(fakeViewmodel, ref fakeProduct);

            Assert.AreEqual(fakeViewmodel.Id, fakeProduct.Id);
            Assert.AreEqual(fakeViewmodel.Name, fakeProduct.Name);
            Assert.AreEqual(fakeViewmodel.ReleaseDate, fakeProduct.ReleaseDate);
            Assert.AreEqual(fakeViewmodel.Notes, fakeProduct.Notes);
            Assert.AreEqual(fakeViewmodel.FailOnNullHardwareIdentifier, fakeProduct.FailOnNullHardwareIdentifier);
            Assert.AreEqual(Enum.Parse(typeof(VersionLeewayType), fakeViewmodel.VersionLeeway), fakeProduct.VersionLeeway);

            var expectedActivationType = ActivationMethodType.None;
            foreach (var activation_type in fakeViewmodel.PermittedActivationTypes)
                expectedActivationType |= (ActivationMethodType)Enum.Parse(typeof(ActivationMethodType), activation_type);
            Assert.AreEqual(expectedActivationType, fakeProduct.PermittedActivationTypes);

            var expectedLockProps = LockPropertyType.None;
            foreach (var lock_prop in fakeViewmodel.SelectedDefaultLockProperties)
                expectedLockProps |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_prop);
            Assert.AreEqual(expectedLockProps, fakeProduct.DefaultLockProperties);
        }

        [Test]
        public void ViewmodelToEntity_FromEntity_ValidModel_ReturnsExpectedResult()
        {
            converter.ViewmodelToEntity(fakeViewmodel, ref fakeProduct);

            // make sure the properties are what we expect
            Assert.AreEqual(fakeViewmodel.Id, fakeProduct.Id);
            Assert.AreEqual(fakeViewmodel.Name, fakeProduct.Name);
            Assert.AreEqual(fakeViewmodel.ReleaseDate, fakeProduct.ReleaseDate);
            Assert.AreEqual(fakeViewmodel.Notes, fakeProduct.Notes);
            Assert.AreEqual(fakeViewmodel.FailOnNullHardwareIdentifier, fakeProduct.FailOnNullHardwareIdentifier);
            Assert.AreEqual(Enum.Parse(typeof(VersionLeewayType), fakeViewmodel.VersionLeeway), fakeProduct.VersionLeeway);

            var expectedActivationType = ActivationMethodType.None;
            foreach (var activation_type in fakeViewmodel.PermittedActivationTypes)
                expectedActivationType |= (ActivationMethodType)Enum.Parse(typeof(ActivationMethodType), activation_type);
            Assert.AreEqual(expectedActivationType, fakeProduct.PermittedActivationTypes);

            var expectedLockProps = LockPropertyType.None;
            foreach (var lock_prop in fakeViewmodel.SelectedDefaultLockProperties)
                expectedLockProps |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_prop);
            Assert.AreEqual(expectedLockProps, fakeProduct.DefaultLockProperties);

            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeProduct.PublicKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeProduct.PrivateKey));
        }
    }
}
