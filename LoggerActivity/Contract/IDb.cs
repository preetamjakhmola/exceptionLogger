using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerActivity
{
    public interface IDb:ILogger
    {
        
        /// <summary>
        /// Name of ConnectionString Mentioned in Web config or App config.
        /// </summary>
        string ConnectionStringName { get; set; } 
    }
}
