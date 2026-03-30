using System;
using System.IO;
using System.Security.Cryptography;
using MHS.Badbir.NetTiers.Entities;

namespace MHS.Badbir.NetTiers.Data
{
    /// <summary>
    /// encryption and decryption code
    /// http://www.codeproject.com/KB/cs/Data_Encryption.aspx
    /// </summary>
    public class bbCrypto
    {
        private string Password = "^Fr%lq(!nzJA}<h*&B/.AXtN$ya@C~GT";

        /// <summary>
        /// 	Encrypts a string, returning a string
        /// </summary>
        /// <value>This type is string.</value>
        /// <remarks>
        /// nulls are returned as nulls. 
        /// </remarks>
        public string encrypt(string InputText)
        {
            if (string.IsNullOrEmpty(InputText)) return ""; // only strings are being encrypted and a string field can't be set to NULL on the website, so convert nulls to empty strings

            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            RijndaelCipher.BlockSize = 256;

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = System.Text.Encoding.ASCII.GetBytes(Password.Length.ToString());
            string EncryptedData;

            //This class uses an extension of the PBKDF1 algorithm defined in the PKCS#5 v2.0 
            //standard to derive bytes suitable for use as key material from a password. 
            //The standard is documented in IETF RRC 2898.

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(32));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                //Defines a stream that links data streams to cryptographic transformations
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(PlainText, 0, PlainText.Length);
                    //Writes the final state and clears the buffer
                    cryptoStream.FlushFinalBlock();
                    byte[] CipherBytes = memoryStream.ToArray();
                    EncryptedData = Convert.ToBase64String(CipherBytes);
                }
            }
            return EncryptedData;
        }

        /// <summary>
        /// 	Decrypts a string, returning a string
        /// </summary>
        /// <value>This type is string.</value>
        /// <remarks>
        /// nulls are returned as nulls. 
        /// </remarks>
        public string decrypt(string InputText)
        {
            if (string.IsNullOrEmpty(InputText)) return ""; // only strings are being encrypted, so a null in the database will be presented as an empty string on the website

            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            RijndaelCipher.BlockSize = 256;

            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = System.Text.Encoding.ASCII.GetBytes(Password.Length.ToString());
            string DecryptedData;

            //Making of the key for decryption
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric Rijndael decryptor object.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(32));
            using (MemoryStream memoryStream = new MemoryStream(EncryptedData))
            {
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                {
                    byte[] PlainText = new byte[EncryptedData.Length];
                    int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                    DecryptedData = System.Text.Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
                }
            }
            //Converting to string
            return DecryptedData;
        }

        public void DecryptPatient(ref BbPatient myPatient)
        {
            bbCrypto myCrypto = new bbCrypto();

            myPatient.Phrn = myCrypto.decrypt(myPatient.Phrn);
            myPatient.Pnhs = myCrypto.decrypt(myPatient.Pnhs);
            myPatient.Title = myCrypto.decrypt(myPatient.Title);
            myPatient.Surname = myCrypto.decrypt(myPatient.Surname);
            myPatient.Forenames = myCrypto.decrypt(myPatient.Forenames);
            myPatient.Address1 = myCrypto.decrypt(myPatient.Address1);
            myPatient.Address2 = myCrypto.decrypt(myPatient.Address2);
            myPatient.Address3 = myCrypto.decrypt(myPatient.Address3);
            myPatient.AddressTown = myCrypto.decrypt(myPatient.AddressTown);
            myPatient.AddressCounty = myCrypto.decrypt(myPatient.AddressCounty);
            myPatient.AddressPostcode = myCrypto.decrypt(myPatient.AddressPostcode);
            myPatient.Phone = myCrypto.decrypt(myPatient.Phone);
            myPatient.Emailaddress = myCrypto.decrypt(myPatient.Emailaddress);
            myPatient.Countryresidence = myCrypto.decrypt(myPatient.Countryresidence);
        }
        /*
        public void DecryptPatientLifestyle(ref BbPatientLifestyle myPatientLifestyle)
        {
            bbCrypto myCrypto = new bbCrypto();

            myPatientLifestyle.Birthtown = myCrypto.decrypt(myPatientLifestyle.Birthtown);
            myPatientLifestyle.Birthcountry = myCrypto.decrypt(myPatientLifestyle.Birthcountry);
        }*/

    }
}