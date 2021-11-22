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
