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

            fakeViewmodel = new ProductViewModel
            {
                Id = NumberFaker.Number(1, int.MaxValue),
                Name = StringFaker.Alpha(20),
                ReleaseDate = DateTime.Now,
                Notes = TextFaker.Sentences(5),
                VersioningStyle = BooleanFaker.Boolean() ? ((int)VersioningStyle.DotNet).ToString() : ((int)VersioningStyle.Semantic).ToString(),
                SelectedDefaultLockProperties = new List<string> { ((int)LockPropertyType.CPU).ToString(), ((int)LockPropertyType.BIOS).ToString() },
                FailOnNullHardwareIdentifier = BooleanFaker.Boolean(),
                PermittedActivationTypes = new List<string> { ((int)ActivationType.Manual).ToString(), ((int)ActivationType.Email).ToString() },
                // randomly selects a versionleewaytype
                VersionLeeway = ((int)VersionLeewayType.Strict).ToString()
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
            Assert.AreEqual(fakeProduct.VersioningStyle, Enum.Parse(typeof(VersioningStyle), vm.VersioningStyle));

            Assert.AreEqual(fakeProduct.FailOnNullHardwareIdentifier, vm.FailOnNullHardwareIdentifier);
            Assert.AreEqual(fakeProduct.VersionLeeway, Enum.Parse(typeof(VersionLeewayType), vm.VersionLeeway));

            // the following two asserts are a little more tricky.
            // we have to convert the list of enums (stored in the view model)
            // to a single value using bitwise math.
            var actualLockProperties = LockPropertyType.None;
            foreach (var lock_property in vm.SelectedDefaultLockProperties)
                actualLockProperties |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_property);

            Assert.AreEqual(fakeProduct.DefaultLockProperties, actualLockProperties);

            var actualActivationTypes = ActivationType.None;
            foreach (var activation_type in vm.PermittedActivationTypes)
                actualActivationTypes |= (ActivationType)Enum.Parse(typeof(ActivationType), activation_type);

            Assert.AreEqual(fakeProduct.PermittedActivationTypes, actualActivationTypes);
        }

        [Test(Description = "ViewmodelToEntity requires a single, non-null argument")]
        public void ViewmodelToEntity_NullViewmodel_ThrowsException()
        {
            // test the first overload
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

            // test our second overload
            try
            {
                ProductViewModel vm = null;
                converter.ViewmodelToEntity(vm);
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
            var entity = converter.ViewmodelToEntity(fakeViewmodel);

            Assert.AreEqual(fakeViewmodel.Id, entity.Id);
            Assert.AreEqual(fakeViewmodel.Name, entity.Name);
            Assert.AreEqual(fakeViewmodel.ReleaseDate, entity.ReleaseDate);
            Assert.AreEqual(fakeViewmodel.Notes, entity.Notes);
            Assert.AreEqual(Enum.Parse(typeof(VersioningStyle), fakeViewmodel.VersioningStyle), entity.VersioningStyle);
            Assert.AreEqual(fakeViewmodel.FailOnNullHardwareIdentifier, entity.FailOnNullHardwareIdentifier);
            Assert.AreEqual(Enum.Parse(typeof(VersionLeewayType), fakeViewmodel.VersionLeeway), entity.VersionLeeway);

            var expectedActivationType = ActivationType.None;
            foreach (var activation_type in fakeViewmodel.PermittedActivationTypes)
                expectedActivationType |= (ActivationType)Enum.Parse(typeof(ActivationType), activation_type);
            Assert.AreEqual(expectedActivationType, entity.PermittedActivationTypes);

            var expectedLockProps = LockPropertyType.None;
            foreach (var lock_prop in fakeViewmodel.SelectedDefaultLockProperties)
                expectedLockProps |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_prop);
            Assert.AreEqual(expectedLockProps, entity.DefaultLockProperties);
        }

        [Test(Description = "ViewmodelToEntity requires two, non-null arguments.  Additionally, the entity.Id property cannot be 0 (indicates it is not already in the database)")]
        public void ViewmodelToEntity_FromEntity_NullViewmodelOrEntity_ThrowsException()
        {
            var exceptions = new List<Exception>();

            try
            {
                converter.ViewmodelToEntity(null, fakeProduct);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }

            try
            {
                converter.ViewmodelToEntity(fakeViewmodel, null);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }

            try
            {
                converter.ViewmodelToEntity(null, null);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }

            var nullArgumentExceptionCount =
                exceptions.Where(e => e.GetType() == typeof(ArgumentNullException)).Count();

            Assert.AreEqual(3, nullArgumentExceptionCount);
        }

        [Test()]
        public void ViewmodelToEntity_FromEntity_ValidModel_ReturnsExpectedResult()
        {
            var entity = converter.ViewmodelToEntity(fakeViewmodel, fakeProduct);

            // make sure the returned instance is the same as the entity passed to the function
            Assert.AreSame(entity, fakeProduct);

            // make sure the properties are what we expect
            Assert.AreEqual(fakeViewmodel.Id, entity.Id);
            Assert.AreEqual(fakeViewmodel.Name, entity.Name);
            Assert.AreEqual(fakeViewmodel.ReleaseDate, entity.ReleaseDate);
            Assert.AreEqual(fakeViewmodel.Notes, entity.Notes);
            Assert.AreEqual(Enum.Parse(typeof(VersioningStyle), fakeViewmodel.VersioningStyle), entity.VersioningStyle);
            Assert.AreEqual(fakeViewmodel.FailOnNullHardwareIdentifier, entity.FailOnNullHardwareIdentifier);
            Assert.AreEqual(Enum.Parse(typeof(VersionLeewayType), fakeViewmodel.VersionLeeway), entity.VersionLeeway);

            var expectedActivationType = ActivationType.None;
            foreach (var activation_type in fakeViewmodel.PermittedActivationTypes)
                expectedActivationType |= (ActivationType)Enum.Parse(typeof(ActivationType), activation_type);
            Assert.AreEqual(expectedActivationType, entity.PermittedActivationTypes);

            var expectedLockProps = LockPropertyType.None;
            foreach (var lock_prop in fakeViewmodel.SelectedDefaultLockProperties)
                expectedLockProps |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lock_prop);
            Assert.AreEqual(expectedLockProps, entity.DefaultLockProperties);

            Assert.IsFalse(string.IsNullOrWhiteSpace(entity.PublicKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(entity.PrivateKey));
        }
    }
}
