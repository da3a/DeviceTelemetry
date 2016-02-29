using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Serialization;

namespace DeviceTelemetry.Library.Transport
{
    public class ConcreteTransport: ITransport
    {
        private string folderPath = "c:\\dawa\\data\\";
        private readonly ISerialize _serializer;
        private readonly ILogger _logger;
        private readonly IDevice _device;
        private bool _disposed = false;

        public ConcreteTransport(ISerialize serializer, ILogger logger, IDevice device)
        {
            _serializer = serializer;
            _logger = logger;
            _device = device;

        }
        public void Open()
        {
            if (string.IsNullOrWhiteSpace(_device.DeviceId))
            {
                throw new ArgumentException("DeviceID value cannot be missing, null, or whitespace");
            }
        }

        public async Task CloseAsync()
        {
            await Task.Delay(1);
        }

        public async Task SendEventAsync(dynamic eventData)
        {
            byte[] bytes;

            bytes = _serializer.SerializeObject(eventData);

            var eventId = Guid.NewGuid();
            string filePath = Path.Combine(folderPath, _device.DeviceId, eventId + ".txt");
            await Task.Run(() => File.WriteAllBytes(filePath, bytes));
        }

        public async Task SendEventAsync(Guid eventId, dynamic eventData)
        {
            await Task.Delay(1);
        }

        public async Task SendEventBatchAsync(IEnumerable<IMessage> messages)
        {
            await Task.Delay(1);
        }
    }
}
