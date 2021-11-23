using SharpDX.DXGI;
using SharpDX.Direct3D;
namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct SRVSettings
    {
        public Format format;
        public ShaderResourceViewDimension dimension;

        public SRVSettings(Format format, ShaderResourceViewDimension dimension)
        {
            this.format = format;
            this.dimension = dimension;
        }

        public static SRVSettings StructuredBufferStandard = new SRVSettings(Format.Unknown, ShaderResourceViewDimension.Buffer);
    }
}
