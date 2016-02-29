using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTelemetry.Library.Devices;

namespace DeviceTelemetry.Library.Telemetry
{
    public interface ITelemetryFactory
    {
        /// <summary>
        /// Populates a device with telemetry events or logic
        /// </summary>
        /// <param name="device">Device interface to populate</param>
        /// <returns>
        /// Returns object as a way to handle returning the instance that is generating telemetry data
        /// so that it can be used by the caller of this method
        /// </returns>
        object PopulateDeviceWithTelemetryEvents(IDevice device);
    }
}
