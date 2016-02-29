using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Telemetry;

namespace DeviceTelemetry.Library.Devices
{
    public interface IDevice
    {
        string DeviceId { get; set; }

        List<ITelemetry> TelemetryEvents { get; set; }

        void Init(InitialDeviceConfig config);

        Task StartAsync(CancellationToken token);  

    }


}
