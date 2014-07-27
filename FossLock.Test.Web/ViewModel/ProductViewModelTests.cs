using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Web.ViewModels;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.ViewModel
{
    [TestFixture]
    internal class ProductViewModelTests
    {
        private ProductViewModel vm = null;

        [SetUp]
        public void SetUp()
        {
            this.vm = new ProductViewModel();
        }
    }
}
