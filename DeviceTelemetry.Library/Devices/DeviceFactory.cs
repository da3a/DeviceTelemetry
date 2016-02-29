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
    public class DeviceFactory: IDeviceFactory
    {
        public IDevice CreateDevice(ILogger logger, ITransportFactory transportFactory, ITelemetryFactory telemetryFactory,
            InitialDeviceConfig config)
        {
            var device = new ConcreteDevice(logger, transportFactory, telemetryFactory);

            device.Init(config);

            return device;
        }
    }
}
