using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;
using DeviceTelemetry.Library.Logging;

namespace DeviceTelemetry.Library.Telemetry
{
    public class ConcreteTelemetryFactory:ITelemetryFactory
    {
        private readonly ILogger _logger;

        public ConcreteTelemetryFactory(ILogger logger)
        {
            _logger = logger;
        }
        public object PopulateDeviceWithTelemetryEvents(IDevice device)
        {
            var monitorTelemetry = new DeviceTelemetry(_logger, device.DeviceId);
            device.TelemetryEvents.Add(monitorTelemetry);
            return monitorTelemetry;

        }
    }
}
