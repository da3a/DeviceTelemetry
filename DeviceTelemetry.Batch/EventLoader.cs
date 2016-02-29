using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Telemetry;
using DeviceTelemetry.Library.Transport;

namespace DeviceTelemetry.Batch
{
    public class EventLoader
    {
        private IDeviceFactory _deviceFactory;
        private ILogger _logger;
        private ITransportFactory _transportFactory;
        private ITelemetryFactory _telemetryFactory;


        public EventLoader(IDeviceFactory deviceFactory, ILogger logger, ITransportFactory transportFactory, ITelemetryFactory telemetryFactory )
        {
            _deviceFactory = deviceFactory;
            _logger = logger;
            _transportFactory = transportFactory;
            _telemetryFactory = telemetryFactory;
        }

        public async  Task  ProcessDeviceAsync(CancellationToken token)
        {
            //create a device

            InitialDeviceConfig config = new InitialDeviceConfig() {DeviceId = "TestDevice"};

            IDevice device = _deviceFactory.CreateDevice(_logger, _transportFactory, _telemetryFactory, config);

            await device.StartAsync(token);

        }
    }
}
