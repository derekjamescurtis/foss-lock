using System;
using System.IO;
using System.Xml;
using LicenseHandler.Crypto;

namespace FossLock.LicenseHandler
{
	public class LicenseHandler
	{
		private IFossLockCrypto gCrypto;
		private XmlDocument doc;
		private string gPathToLicenseFile;
		
		public LicenseHandler (IFossLockCrypto crypto, String pathToLicenseFile)
		{
			gCrypto = crypto;
			gPathToLicenseFile = pathToLicenseFile;
			doc = gCrypto.decryptXml(gPathToLicenseFile);		
			
		}
		
		public string getCustomerName(){
			XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
			return customerName.InnerXml;			
		}
		
		public string getProduct(){
			XmlNode product = doc.SelectSingleNode("/foss-lock/license/product");
			return product.InnerXml;			
		}
		
		public string getVersion (){
			XmlNode version = doc.SelectSingleNode("/foss-lock/license/version");
			return version.InnerXml;				
		}
		
		public string getRegisteredLevel (){
			XmlNode registeredLevel = doc.SelectSingleNode("/foss-lock/license/registered-level");
			return registeredLevel.InnerXml;				
		}
		
		public string getNetworkLic(){
			XmlNode networkLic = doc.SelectSingleNode("/foss-lock/license/network-lic");
			return networkLic.InnerXml;				
		}
		
		public string getUserDefinedField(){
			XmlNode udf = doc.SelectSingleNode("/foss-lock/license/user-defined-field");
			return udf.InnerXml;				
		}
	}
}

