using System;
using System.IO;
using LicenseHandler.Crypto;
using System.Xml;


namespace FossLock.LicenseHandler.Crypto
{
	/// <summary>
	/// Class that inmplements FlossLockCryptoInterface. 
	/// This class is able to read in a plain text license file. 
	/// </summary>

	public class FossLockNoEncryption : IFossLockCrypto 
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

