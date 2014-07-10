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
    internal class ProductEntityConverterTests
    {
        private ProductEntityConverter converter = null;
        private Product fakeProduct = null;

        /// <summary>
        ///     Reinitializes converter and fakeProduct before each test.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            converter = new ProductEntityConverter();

            fakeProduct = new Product
            {
                Id = NumberFaker.Number(1, int.MaxValue),
                Name = StringFaker.Alpha(20),
                ReleaseDate = DateTime.Now,
                Notes = TextFaker.Sentences(5),
                VersioningStyle = BooleanFaker.Boolean() ? VersioningStyle.DotNet : VersioningStyle.Semantic,
                LicenseEncryptionType = EncryptionType.RSA_4096,
                PrivateKey = StringFaker.AlphaNumeric(50),
                PublicKey = StringFaker.AlphaNumeric(50),
                DefaultLockProperties = LockPropertyType.CPU | LockPropertyType.BIOS,
                FailOnNullHardwareIdentifier = BooleanFaker.Boolean(),
                PermittedActivationTypes = ActivationType.Manual | ActivationType.Email,
                PermittedExpirationTypes = ExpirationType.Service,
                VersionLeeway = VersionLeewayType.Strict,
            };
        }

        [Test(Description = "EntityToViewmodel requires a single, non-null argument.")]
        public void EntityToViewmodel_NullEntity_ThrowsException()
        {
            try
            {
                Product p = null;
                var vm = converter.EntityToViewmodel(p);
                Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
            }
        }

        [Test(Description = "ProductViewModel attributes set properly after conversion")]
        public void EntityToViewmodel_ValidEntity_ReturnsExpectedViewmodel()
        {
            var vm = converter.EntityToViewmodel(fakeProduct);

            Assert.AreEqual(fakeProduct.Id, vm.Id);
            Assert.AreEqual(fakeProduct.Name, vm.Name);
            Assert.AreEqual(fakeProduct.ReleaseDate, vm.ReleaseDate);
            Assert.AreEqual(fakeProduct.Notes, vm.Notes);
            Assert.AreEqual(fakeProduct.VersioningStyle, vm.VersioningStyle);

            Assert.AreEqual(fakeProduct.FailOnNullHardwareIdentifier, vm.FailOnNullHardwareIdentifier);
            Assert.AreEqual(fakeProduct.VersionLeeway, vm.VersionLeeway);

            // the following two asserts are a little more tricky.
            // we have to convert the list of enums (stored in the view model)
            // to a single value using bitwise math.
            var actualLockProperties = LockPropertyType.None;
            foreach (var lock_property in vm.SelectedDefaultLockProperties)
                actualLockProperties |= lock_property;

            Assert.AreEqual(fakeProduct.DefaultLockProperties, actualLockProperties);

            var actualActivationTypes = ActivationType.None;
            foreach (var activation_type in vm.PermittedActivationTypes)
                actualActivationTypes |= activation_type;

            Assert.AreEqual(fakeProduct.PermittedActivationTypes, actualActivationTypes);

            // the next few checks are to make sure the selected flag is set
            // in the AllActivationTypes/AllLockProperties
            // this is a little logical insanity in how Razor works imho..
            foreach (var activation_type in vm.AllActivationTypes)
            {
                var enum_value = (ActivationType)Enum.Parse(typeof(ActivationType), activation_type.Value);
                if (fakeProduct.PermittedActivationTypes.HasFlag(enum_value))
                {
                    Assert.IsTrue(activation_type.Selected,
                        string.Format("{0} present in PermittedActivationTypes, but AllActivationTypes missing Selected flag", activation_type.Text));
                }
            }

            foreach (var lock_prop in vm.AllLockProperties)
            {
                var enum_value = (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_prop.Value);
                if (fakeProduct.DefaultLockProperties.HasFlag(enum_value))
                {
                    Assert.IsTrue(lock_prop.Selected,
                        string.Format("{0} present in DefaultLockProperties, but AllLockProperties missing Selected flag", lock_prop.Text));
                }
            }
        }

        [Test(Description = "ViewmodelToEntity requires a single, non-null argument")]
        public void ViewmodelToEntity_NullViewmodel_ThrowsException()
        {
            try
            {
                ProductViewModel vm = null;
                var p = converter.ViewmodelToEntity(vm);
                Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("viewmodel", ((ArgumentNullException)ex).ParamName);
            }
        }

        [Test(Description = "Product attributes set properly after conversion.")]
        public void ViewmodelToEntity_ValidViewmodel_ReturnsExpectedResult()
        {
            Assert.Inconclusive("Not implemented");
        }
    }
}
