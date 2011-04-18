using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BergenAmee
{
    /// <summary>
    /// Simple Stub logging class
    /// </summary>
    public class LogHelper
    {
        public static String logEntry;

        /// <summary>
        /// Stub method for writing a normal logfile entry
        /// </summary>
        /// <param name="logMessage"></param>        
        public static void Log(String logMessage)
        {
            Console.Out.WriteLine(logMessage);
        }

        /// <summary>
        /// Stub method for writing an error logfile entry
        /// </summary>
        /// <param name="logMessage"></param>        
        public static void LogError(String errorMessage)
        {
            Console.Error.WriteLine(errorMessage);
        }

    }
}
