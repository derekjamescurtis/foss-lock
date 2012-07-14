using System;
using System.IO;
using System.Xml;

namespace LicenseHandler.Crypto
{
	public interface FossLockCryptoInterface
	{
			XmlDocument decryptXml(string fileName);
	
			void encryptXml(string fileName, XmlDocument inputDoc);
		
 
	}
}

