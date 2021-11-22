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
    /// <summary>
    /// A structured buffer
    /// </summary>
    public struct StructuredBuffer
    {
        public int bufferSize;
        public SharpDX.Direct3D11.Buffer buffer;
        public ShaderResourceView srv;
        public StructuredBuffer(int bufferSize, SharpDX.Direct3D11.Buffer buffer, ShaderResourceView srv)
        {
            this.bufferSize = bufferSize;
            this.buffer = buffer;
            this.srv = srv;
        }
    }
}
