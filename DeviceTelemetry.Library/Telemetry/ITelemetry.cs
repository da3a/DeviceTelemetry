using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceTelemetry.Library.Telemetry
{
    public  interface ITelemetry
    {
        Task SendEventsAsync(CancellationToken token, Func<object, Task> sendMessageAsync);
    }
}