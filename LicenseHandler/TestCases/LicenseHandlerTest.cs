using System;
using NUnit.Framework;
using FossLock.LicenseHandler.Crypto;
using FossLock.LicenseHandler;

namespace FossLock.LicenseHandler
{
	[TestFixture()]
	public class LicenseHandlerTest
	{
		string sampleFile = "<?xml version=\"1.0\"?>" +
			"<foss-lock><license>" +
			"<customer-name>Sample Customer</customer-name>" +
			"<product-name>Some Product</product-name>" +
			"<product-id>123456</product-id>" +
			"<product-version>123</product-version>" +
			"<registered-level>111</registered-level>" +
			"<network-lic>yes</network-lic>" +
			"<user-defined-field>User Defined Field</user-defined-field>" +
			"</license></foss-lock>";
		
		[Test()]
		public void CustomerName()
		{
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.CustomerName, "Sample Customer");
		}
		
		[Test()]
		public void ProductName(){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.ProductName , "Some Product");
		}
		
		[Test()]
		public void ProductID(){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.ProductID, "123456");
		}
		
		[Test()]
		public void ProductVersion (){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.ProductVersion, "123");			
		}
		
		[Test()]
		public void RegisteredLevel (){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.RegisterLevel, "111");			
		}
		
		[Test()]
		public void NetworkLic(){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.NetworkLic, "yes");			
		}
		
		[Test()]
		public void getUserDefinedField(){
			IFossLockCrypto locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.UserDefinedField, "User Defined Field");			
		}
	}
}

