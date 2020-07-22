using LoggerActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LoggerActivity.DbLogger<LoggerActivity.MSSQLServer>;
namespace ExceptionHandling
{
    class Program
    {

        static void Main(string[] args)
        {
            Log.ConnectionStringName = "ConString";
            try
            {

            }
            catch (Exception ex)

            {
                ex.OnException();
            }
             
            NLogger.Log.Info(DateTime.Now.ToString()) ;
              Log.Error("Failed to safe data, Failed");

            //NLogger.Log.Warning("This is warning");
            //NLogger.Log.Error("this is error");
            //NLogger.Log.Fatal("Fatal error");

        }
    }


}
