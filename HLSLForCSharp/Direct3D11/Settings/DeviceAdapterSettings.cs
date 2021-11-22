using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct DeviceAdapterSettings
    {
        public Adapter adapter;
        public DeviceCreationFlags creationFlags;
        public FeatureLevel[] featureLevels;

        public DeviceAdapterSettings(Adapter adapter, DeviceCreationFlags creationFlags, params FeatureLevel[] featureLevels)
        {
            this.adapter = adapter;
            this.creationFlags = creationFlags;
            this.featureLevels = featureLevels;
        }
    }
}
