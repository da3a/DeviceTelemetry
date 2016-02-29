using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Logging;
using Newtonsoft.Json.Linq;

namespace DeviceTelemetry.Library.Telemetry
{
    public class DeviceTelemetry:ITelemetry
    {
        private const int REPORT_FREQUENCY_IN_SECONDS = 5;
        public bool TelemetryActive { get; set; }

        private readonly ILogger _logger;
        private readonly string _deviceId;

        public DeviceTelemetry(ILogger logger, string deviceId)
        {
            _logger = logger;
            _deviceId = deviceId;
            TelemetryActive = true;
        }

        public async Task SendEventsAsync(CancellationToken token, Func<object, Task> sendMessageAsync)
        {
            Random random = new Random(10);

            while (!token.IsCancellationRequested)
            {
                if (TelemetryActive)
                {
                    var messageBody = new JObject();
                    messageBody.Add("TestField", random.Next());
                    await sendMessageAsync(messageBody);
                    _logger.LogInfo("Sending " + messageBody + " for Device: " + _deviceId);
                }
                await Task.Delay(TimeSpan.FromSeconds(REPORT_FREQUENCY_IN_SECONDS), token);
            }
        }
    }
}
