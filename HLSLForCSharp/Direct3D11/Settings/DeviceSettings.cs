using SharpDX.Direct3D;
using SharpDX.Direct3D11;
namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct DeviceSettings
    {
        public DriverType driverType;
        public DeviceCreationFlags creationFlags;
        public FeatureLevel[] featureLevels;
        public DeviceSettings(DriverType driverType, DeviceCreationFlags creationFlags, params FeatureLevel[] featureLevels)
        {
            this.driverType = driverType;
            this.creationFlags = creationFlags;
            this.featureLevels = featureLevels;
        }
    }
}
