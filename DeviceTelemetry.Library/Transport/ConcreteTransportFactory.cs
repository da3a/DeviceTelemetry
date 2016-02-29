using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Serialization;

namespace DeviceTelemetry.Library.Transport
{
    public class ConcreteTransportFactory: ITransportFactory
    {
        private ISerialize _serializer;
        private ILogger _logger;

        public ConcreteTransportFactory(ISerialize serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
        }
        public ITransport CreateTransport(IDevice device)
        {
            return new ConcreteTransport(_serializer, _logger, device);
        }
    }
}
