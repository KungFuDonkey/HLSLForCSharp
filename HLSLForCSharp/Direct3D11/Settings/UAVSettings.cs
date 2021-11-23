using SharpDX.DXGI;
using SharpDX.Direct3D11;
namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct UAVSettings
    {
        public Format format;
        public UnorderedAccessViewDimension dimension;
        public UAVSettings(Format format, UnorderedAccessViewDimension dimension)
        {
            this.format = format;
            this.dimension = dimension;
        }

        public static UAVSettings RWBufferStandard = new UAVSettings(Format.Unknown, UnorderedAccessViewDimension.Buffer);
    }
}
