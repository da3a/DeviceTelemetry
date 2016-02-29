using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTelemetry.Library.Transport
{
    public interface ITransport
    {
        void Open();

        Task CloseAsync();

        Task SendEventAsync(dynamic eventData);

        Task SendEventAsync(Guid eventId, dynamic eventData);

        Task SendEventBatchAsync(IEnumerable<IMessage> messages);
    }
}
