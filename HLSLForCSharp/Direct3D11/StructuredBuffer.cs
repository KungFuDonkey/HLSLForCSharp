using SharpDX.Direct3D11;

namespace HLSLForCSharp.Direct3D11
{
    /// <summary>
    /// A structured buffer
    /// </summary>
    public struct StructuredBuffer
    {
        public int bufferSize;
        public Buffer buffer;
        public ShaderResourceView srv;
        public StructuredBuffer(int bufferSize, Buffer buffer, ShaderResourceView srv)
        {
            this.bufferSize = bufferSize;
            this.buffer = buffer;
            this.srv = srv;
        }
    }
}
