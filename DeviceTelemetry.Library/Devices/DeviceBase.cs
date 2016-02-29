using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Logging;
using DeviceTelemetry.Library.Telemetry;
using DeviceTelemetry.Library.Transport;

namespace DeviceTelemetry.Library.Devices
{
    public class DeviceBase:IDevice
    {
        private int _currentEventGroup = 0;
        private readonly ILogger _logger;
        private readonly ITransportFactory _transportFactory;
        private readonly ITelemetryFactory _telemetryFactory;
        protected ITransport Transport;
        protected object _telemetryController;

        public string DeviceId { get; set; }
        public List<ITelemetry> TelemetryEvents { get; set; }
        public bool RepeatEventListForever { get; set; }


        /// </summary>
        /// <param name="logger">Logger where this device will log information to</param>
        /// <param name="transport">Transport where the device will send and receive data to/from</param>
        /// <param name="config">Config to start this device with</param>
        public DeviceBase(ILogger logger, ITransportFactory transportFactory, ITelemetryFactory telemetryFactory)
        {
            _logger = logger;
            _transportFactory = transportFactory;
            _telemetryFactory = telemetryFactory;
            TelemetryEvents = new List<ITelemetry>();
        }

        public void Init(InitialDeviceConfig config)
        {
            this.DeviceId = config.DeviceId;
            Transport = _transportFactory.CreateTransport(this);
            _telemetryController = _telemetryFactory.PopulateDeviceWithTelemetryEvents(this);
        }

        public async Task StartAsync(CancellationToken token)
        {

            try
            {
                Transport.Open();

                var loopTasks = new List<Task>
                {
                    StartSendLoopAsync(token)
                };

                await Task.WhenAll(loopTasks.ToArray());

                await Transport.CloseAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected Exception starting device: {0}", ex.ToString());
                
            }
        }

        /// <summary>
        /// Iterates through the list of IEventGroups and fires off the events in a given event group before moving to the next.
        /// If RepeatEventListForever is true the device will continue to loop through each event group, if false
        /// once a single pass is made through all event groups the device will stop sending events
        /// </summary>
        /// <param name="token">Cancellation token to cancel out of the loop</param>
        /// <returns></returns>
        private async Task StartSendLoopAsync(CancellationToken token)
        {
            try
            {
            do
            {
                _currentEventGroup = 0;

                _logger.LogInfo("Starting events list for device {0}...", DeviceId);

                while (_currentEventGroup < TelemetryEvents.Count && !token.IsCancellationRequested)
                {
                    _logger.LogInfo("Device {0} starting IEventGroup {1}...", DeviceId, _currentEventGroup);

                    var eventGroup = TelemetryEvents[_currentEventGroup];

                    await eventGroup.SendEventsAsync(token, async (object eventData) =>
                    {
                        await Transport.SendEventAsync(eventData);
                    });

                    _currentEventGroup++;
                }

                _logger.LogInfo("Device {0} finished sending all events in list...", DeviceId);

            } while (RepeatEventListForever && !token.IsCancellationRequested);

            _logger.LogWarning("Device {0} sent all events and is shutting down send loop. (Set RepeatEventListForever = true on the device to loop forever.)", DeviceId);

            }
            catch (TaskCanceledException)
            {
                //do nothing if the task was cancelled
            }
            catch (Exception ex)
            {
               _logger.LogError("Unexpected Exception starting device send loop: {0}", ex.ToString());
            }

            if (token.IsCancellationRequested)
            {
                _logger.LogInfo("********** Processing Device {0} has been cancelled - StartSendLoopAsync Ending. **********", DeviceId);
            }

        }
    }
}
