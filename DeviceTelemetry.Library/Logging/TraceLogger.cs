using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTelemetry.Library.Logging
{
    public class TraceLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Trace.TraceInformation(message);
        }

        public void LogInfo(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void LogWarning(string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
        }

        public void LogError(string message)
        {
            Trace.TraceError(message);
        }

        public void LogError(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }
    }
}
