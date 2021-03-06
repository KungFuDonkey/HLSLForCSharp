using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using HLSLForCSharp.Direct3D11.Settings;
namespace HLSLForCSharp.Direct3D11
{
    public class DeviceManager
    {
        /// <summary>
        /// Standard device
        /// </summary>
        public static Device device = new Device(DriverType.Hardware, DeviceCreationFlags.SingleThreaded);

        /// <summary>
        /// Change the standard device settings
        /// </summary>
        /// <param name="settings"></param>
        public static void SetDeviceSettings(DeviceSettings settings)
        {
            device = new Device(settings.driverType, settings.creationFlags, settings.featureLevels);
        }

        /// <summary>
        /// Change the standard device settings via adapter
        /// </summary>
        /// <param name="settings"></param>
        public static void SetDeviceSettingsViaAdapter(DeviceAdapterSettings settings)
        {
            device = new Device(settings.adapter, settings.creationFlags, settings.featureLevels);
        }
    }
}
