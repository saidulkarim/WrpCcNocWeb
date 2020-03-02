using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.OleDb;

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
            T t;
            if (tryParse(s, out t))
                return t;
            return null;
        }     
    }
}
