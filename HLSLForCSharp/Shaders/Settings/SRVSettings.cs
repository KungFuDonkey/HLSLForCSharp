using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;

namespace HLSLForCSharp.Shaders.Settings
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
