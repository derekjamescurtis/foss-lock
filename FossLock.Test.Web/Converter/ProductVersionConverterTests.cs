﻿using System;
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
        public void EntityToViewModel_ValidEntity_ReturnsExpectedViewModelState()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ViewModelToEntity_ValidEntity_ReturnsExpectedEntityState()
        {
            Assert.Inconclusive();
        }
    }
}
