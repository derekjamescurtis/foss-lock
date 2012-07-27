using System;
using System.IO;
using System.Xml;
using FossLock.LicenseHandler.Crypto;

namespace FossLock.LicenseHandler
{
	public class LicenseHandler
	{
		private IFossLockCrypto gCrypto;
		private XmlDocument doc;
		
		public LicenseHandler (IFossLockCrypto crypto, String encryptedString)
		{
			gCrypto = crypto;
			doc = gCrypto.decryptXml(encryptedString);		
			
		}
		
<<<<<<< OURS
		public string getEncryptedOutput(){
			return gCrypto.encryptXml(doc);
=======
		public string getCustomerName()
		{
			XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
			return customerName.InnerXml;			
>>>>>>> THEIRS
		}
		
<<<<<<< OURS
		public string CustomerName{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
				customerName.InnerXml = value;
			}
				
=======
		public string getProduct()
		{
			XmlNode product = doc.SelectSingleNode("/foss-lock/license/product");
			return product.InnerXml;			
>>>>>>> THEIRS
		}
		
<<<<<<< OURS
		public string ProductName{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-name");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-name");
				customerName.InnerXml = value;
			}
=======
		public string getVersion ()
		{
			XmlNode version = doc.SelectSingleNode("/foss-lock/license/version");
			return version.InnerXml;				
>>>>>>> THEIRS
		}
		
<<<<<<< OURS
		public string ProductID{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-id");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-id");
				customerName.InnerXml = value;
			}
=======
		public string getRegisteredLevel ()
		{
			XmlNode registeredLevel = doc.SelectSingleNode("/foss-lock/license/registered-level");
			return registeredLevel.InnerXml;				
>>>>>>> THEIRS
		}
		
<<<<<<< OURS
		public string ProductVersion{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-version");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-version");
				customerName.InnerXml = value;
			}
=======
		public string getNetworkLic()
		{
			XmlNode networkLic = doc.SelectSingleNode("/foss-lock/license/network-lic");
			return networkLic.InnerXml;				
>>>>>>> THEIRS
		}
		
<<<<<<< OURS
		public string RegisterLevel{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/registered-level");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/registered-level");
				customerName.InnerXml = value;
			}
		}
		
		public string NetworkLic{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/network-lic");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/network-lic");
				customerName.InnerXml = value;
			}
		}
		
		public string UserDefinedField{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/user-defined-field");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/user-defined-field");
				customerName.InnerXml = value;
			}
=======
		public string getUserDefinedField()
		{
			XmlNode udf = doc.SelectSingleNode("/foss-lock/license/user-defined-field");
			return udf.InnerXml;				
>>>>>>> THEIRS
		}
	}
}

