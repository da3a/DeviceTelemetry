using DeviceTelemetry.Library.Devices;

namespace DeviceTelemetry.Library.Transport
{
    public interface ITransportFactory
    {
        ITransport CreateTransport(IDevice device);
    }
}
