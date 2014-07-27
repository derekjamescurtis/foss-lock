using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Converter
{
    [TestFixture]
    internal class ProductVersionConverterTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void EntityToViewModel_NullEntity_ThrowsException()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void EntityToViewModel_ValidEntity_ReturnsExpectedViewModel()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntity_NullViewModel_ThrowsException()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntity_ValidEntity_ReturnsExpectedResult()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntityFromEntity_NullViewModelOrEntity_ThrowsException()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntityFromEntity_ReturnsExpectedResult()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntityFromEntity_ReturnsSameReference()
        {
            Assert.Inconclusive();
        }
    }
}
