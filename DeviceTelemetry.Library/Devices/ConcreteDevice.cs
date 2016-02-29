using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Telemetry;
using DeviceTelemetry.Library.Transport;

namespace DeviceTelemetry.Library.Devices
{
    public class ConcreteDevice : DeviceBase
    {
        public ConcreteDevice(ILogger logger, ITransportFactory transportFactory,
ITelemetryFactory telemetryFactory)
            : base(logger, transportFactory, telemetryFactory)
        {
        }

    }
}
