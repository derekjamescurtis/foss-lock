using System;
using NUnit.Framework;
using LicenseHandler.Crypto;
using LicenseHandler;

namespace LicenseHandler
{
	[TestFixture()]
	public class LicenseHandlerTest
	{
		string sampleFile = "<?xml version=\"1.0\"?>" +
			"<foss-lock><license>" +
			"<customer-name>Sample Customer</customer-name>" +
			"<product>Some Product</product>" +
			"<version>123456</version>" +
			"<registered-level>111</registered-level>" +
			"<network-lic>yes</network-lic>" +
			"<user-defined-field>User Defined Field</user-defined-field>" +
			"</license></foss-lock>";
		
		[Test()]
		public void getCustomerName()
		{
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getCustomerName(), "Sample Customer");
		}
		
		[Test()]
		public void getProduct(){
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getProduct() , "Some Product");
		}
		
		[Test()]
		public void getVersion (){
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getVersion(), "123456");			
		}
		
		[Test()]
		public void getRegisteredLevel (){
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getRegisteredLevel(), "111");			
		}
		
		[Test()]
		public void getNetworkLic(){
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getNetworkLic(), "yes");			
		}
		
		[Test()]
		public void getUserDefinedField(){
			FossLockCryptoInterface locker = new FossLockStringInput();
			LicenseHandler lh = new LicenseHandler(locker, sampleFile);
			Assert.AreEqual(lh.getUserDefinedField(), "User Defined Field");			
		}
	}
}

