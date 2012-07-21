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
		
		public string getEncryptedOutput(){
			return gCrypto.encryptXml(doc);
		}
		
		public string CustomerName{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/customer-name");
				customerName.InnerXml = value;
			}
				
		}
		
		public string ProductName{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-name");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-name");
				customerName.InnerXml = value;
			}
		}
		
		public string ProductID{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-id");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-id");
				customerName.InnerXml = value;
			}
		}
		
		public string ProductVersion{
			get{
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-version");
				return customerName.InnerXml;			
			}
			
			set {
				XmlNode customerName = doc.SelectSingleNode("/foss-lock/license/product-version");
				customerName.InnerXml = value;
			}
		}
		
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
		}
	}
}

