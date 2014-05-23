using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FossLock.Core;

namespace FossLock.BLL.Util
{
    /// <summary>
    ///     Simple utility function to generate an RSA public/private key pair.
    ///     NOTE: There is a hard dependency between this utility class and
    ///     ProductService this is by design at the current time because I see
    ///     it being overkill to do anything else here.
    /// </summary>
    public class RsaKeypairGenerator
    {
        public KeyPair GenerateKeypair(EncryptionType encryption)
        {
            int modulus;
            switch (encryption)
            {
                case EncryptionType.RSA_512:
                    modulus = 512;
                    break;

                case EncryptionType.RSA_1024:
                    modulus = 1024;
                    break;

                case EncryptionType.RSA_1536:
                    modulus = 1536;
                    break;

                case EncryptionType.RSA_2048:
                    modulus = 2048;
                    break;

                case EncryptionType.RSA_4096:
                    modulus = 4096;
                    break;

                default:
                    throw new ArgumentException("encryption");
            }

            KeyPair kp = null;

            using (var rsa = new RSACryptoServiceProvider(modulus))
            {
                try
                {
                    var privParams = rsa.ExportParameters(true);
                    var pubParams = rsa.ExportParameters(false);

                    var privBlob = rsa.ToXmlString(true);
                    var pubBlob = rsa.ToXmlString(false);

                    kp = new KeyPair
                    {
                        PrivKey = privBlob,
                        PubKey = pubBlob
                    };
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

            return kp;
        }
    }

    public class KeyPair
    {
        public string PubKey { get; set; }

        public string PrivKey { get; set; }
    }
}
