using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;

namespace HLSLForCSharp.Shaders
{
    public struct RWStructuredBuffer
    {
        public int bufferSize;
        public SharpDX.Direct3D11.Buffer buffer;
        public UnorderedAccessView uav;
        public RWStructuredBuffer(int bufferSize, SharpDX.Direct3D11.Buffer buffer, UnorderedAccessView uav)
        {
            this.bufferSize = bufferSize;
            this.buffer = buffer;
            this.uav = uav;
        }
    }
}
