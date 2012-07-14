using System;
using System.IO;
using LicenseHandler.Crypto;
using System.Xml;

namespace LicenseHandler.Crypto
{
	public class FossLockStringInput : FossLockCryptoInterface 
	{
		
		public XmlDocument decryptXml(string fileName){
			XmlDocument doc = new XmlDocument();
			XmlTextReader reader = new XmlTextReader(fileName);
			
			reader.Read(); 
			// load reader 
			doc.Load(reader);
			return doc;
		}

		public void encryptXml(string fileName, XmlDocument inputDoc){
			inputDoc.Save(fileName);
		}
	}
}

