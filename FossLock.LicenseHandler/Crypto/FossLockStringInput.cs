using System;
using System.IO;
using FossLock.LicenseHandler.Crypto;
using System.Xml;
using System.Xml.Linq;

namespace FossLock.LicenseHandler.Crypto
{
	public class FossLockStringInput : IFossLockCrypto 
	{
		
		/// <summary>
		/// Reads a file name as a String and return an XmlDocument.
		/// </summary>
		/// <returns>
		/// XmlDocument as read from the input filename.
		/// </returns>
		/// <param name='fileName'>
		/// Filename that holds license files.
		/// </param>
		public XmlDocument decryptXml(string fileName){
			XDocument doc = XDocument.Parse(fileName);
			
			var xmlDocument = new XmlDocument();
            using(var xmlReader = doc.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
			
			return xmlDocument;
		}
		
		/// <summary>
		/// Encrypts the xml. Does nothing
		/// </summary>
		/// <param name='fileName'>
		/// File name. --Ignored
		/// </param>
		/// <param name='inputDoc'>
		/// Input document. --Ignored
		/// </param>
		public string encryptXml(XmlDocument inputDoc){
			
			StringWriter sw = new StringWriter();
			XmlTextWriter xw = new XmlTextWriter(sw);
			inputDoc.WriteTo(xw);
			return sw.ToString();

		}
	}
}

