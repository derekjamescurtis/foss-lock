using System;
using System.IO;
using System.Xml;

namespace FossLock.LicenseHandler.Crypto
{
	public interface IFossLockCrypto
	{
			XmlDocument decryptXml(string fileName);
	
			void encryptXml(string fileName, XmlDocument inputDoc);
		
 
	}
}

