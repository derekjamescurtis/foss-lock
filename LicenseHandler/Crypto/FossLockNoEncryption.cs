using System;
using System.IO;
using LicenseHandler.Crypto;
using System.Xml;
using System.Xml.Linq;


namespace LicenseHandler.Crypto
{
	public class FossLockNoEncryption : FossLockCryptoInterface 
	{
		public FossLockNoEncryption ()
		{
		}
		
		public XmlDocument decryptXml(string fileName){
			XDocument doc = XDocument.Parse(fileName);
			
			var xmlDocument = new XmlDocument();
            using(var xmlReader = doc.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
			
			return xmlDocument;
		}

		public void encryptXml(string fileName, XmlDocument inputDoc){
			//DO NOTHING
		}
	}
}

