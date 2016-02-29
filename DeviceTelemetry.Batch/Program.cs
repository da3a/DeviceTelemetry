using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Serialization;
using DeviceTelemetry.Library.Telemetry;
using DeviceTelemetry.Library.Transport;

namespace DeviceTelemetry.Batch
{
    class Program
    {

        static CancellationTokenSource cts = new CancellationTokenSource();

        static void Main(string[] args)
        {
            var serializer = new JsonSerialize();
            ILogger logger = new TraceLogger();
            IDeviceFactory deviceFactory = new DeviceFactory();
            ITransportFactory transportFactory = new ConcreteTransportFactory(serializer, logger);
            ITelemetryFactory telemetryFactory = new ConcreteTelemetryFactory(logger);

            EventLoader ev = new EventLoader(deviceFactory, logger, transportFactory, telemetryFactory);

            var t = Task.Run(()=> ev.ProcessDeviceAsync(cts.Token));

            t.Wait();

            Console.ReadLine();
        }
    }
}
