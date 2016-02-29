using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DeviceTelemetry.Library.Transport
{
    //Some kind of custom message
    public interface  IMessage
    {
        string MessageId { get; set; }
        IDictionary<string, string> Properties { get; }
        string To { get; set; }

    }
}