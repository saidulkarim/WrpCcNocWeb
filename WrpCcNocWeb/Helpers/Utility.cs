using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using WrpCcNocWeb.Models.UserManagement;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;

namespace WrpCcNocWeb.Helpers
{
    public static class Utility
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        //public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        //{
        //    OleDbConnection oledbConn = new OleDbConnection(connString);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        oledbConn.Open();
        //        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        oleda.SelectCommand = cmd;
        //        DataSet ds = new DataSet();
        //        oleda.Fill(ds);

        //        dt = ds.Tables[0];
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        oledbConn.Close();
        //    }

        //    return dt;
        //}

        public delegate bool TryParseDelegate<T>(string s, out T t);

        public static T? TryParseNullable<T>(string s, TryParseDelegate<T> tryParse) where T : struct
        {
            if (string.IsNullOrEmpty(s))
                return null;
            if (tryParse(s, out T t))
                return t;
            return null;
        }

        /// <summary>
        /// Purpose: Generating application submission tracking code
        /// Author: A.T.M. Saidul Karim RonY
        /// Date: 12 Mar, 2020
        /// </summary>
        /// <param name="projectId">This is Application Project ID and will come from database</param>
        /// <returns>String type 11 digit tracking code. Pattern: YYMMdd + 5 digit 0 left padded number of Project Id e.g 20031200001</returns>
        public static string GenerateTrackingNumber(this string projectId, int len)
        {
            string result = DateTime.Now.ToString("yyMMdd") + projectId.PadLeft(len, '0');
            return result;
        }

        public static string PassWordHash(this string pass)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(pass));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string EncryptString(this string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(GetPasswordPhrase()));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string GetPasswordPhrase()
        {
            string tmpStr = "CEGIS_IMS_2020";
            string retVal = string.Empty;

            for (int i = tmpStr.Length; i > 0; i--)
            {
                retVal = retVal + tmpStr[i - 1].ToString();
            }

            return retVal;
        }

        public static string DecryptString(this string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(GetPasswordPhrase()));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            try
            {
                // Step 4. Convert the input string to a byte[]
                byte[] DataToDecrypt = Convert.FromBase64String(Message);

                // Step 5. Attempt to decrypt the string            
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            //catch (Exception ex)
            //{
            //    //return UTF8.GetString(Results);
            //}
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}
