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
    }
}
