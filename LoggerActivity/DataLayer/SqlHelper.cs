using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
namespace LoggerActivity
{
    static class SqlHelper
    {
        static string connetionString = null;
        static bool tableExist = false;
        internal static void ExecuteNonQuery(string connetionStringName, string stackTrace, string errorMessage, string errorType = null)
        {
            connetionString = ConfigurationManager.ConnectionStrings[connetionStringName].ConnectionString;

            SqlConnection con;
            SqlCommand cmd;

            con = new SqlConnection(connetionString);
            try
            {

                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                if (!con.IsTableExist())
                {
                    con.CreateTable();
                }
                cmd.InsertLog(errorMessage, errorType, stackTrace);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                con.Close();
            }
        }

        private static void InsertLog(this SqlCommand cmd, string errorMessage, string errorType, string stackTrace)
        {

            cmd.CommandText = "insert into [dbo].[ErrorLogs](ErrorType,ErrorMessage,StackTrace) values(@errorType,@errorMessage,@stackTrace);";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@errorMessage", errorMessage);
            cmd.Parameters.AddWithValue("@errorType", errorType);
            cmd.Parameters.AddWithValue("@stackTrace", stackTrace);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        private static void CreateTable(this SqlConnection con)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[ErrorLogs]([Sno] [numeric](18, 0) IDENTITY(1,1) NOT NULL,[ErrorType] [nchar](10) NOT NULL,	[ErrorMessage] [nvarchar](500) NOT NULL,	[StackTrace] [nvarchar](200) NULL,	[ErrorDate] [datetime] DEFAULT GETDATE(), CONSTRAINT [PK_ErrorLogs] PRIMARY KEY CLUSTERED ([Sno] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];", con))
                    cmd.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private static bool IsTableExist(this SqlConnection con)
        {
            if (tableExist)
                return true;

            using (SqlCommand cmd = new SqlCommand("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where table_name  = 'ErrorLogs'", con))
            {
                object isExist = cmd.ExecuteScalar();

                tableExist = (isExist is null) ? false : true;
                return tableExist;
            }
        }
    }
}
