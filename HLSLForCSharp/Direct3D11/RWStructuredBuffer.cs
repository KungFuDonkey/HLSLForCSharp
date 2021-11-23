using SharpDX.Direct3D11;

namespace HLSLForCSharp.Direct3D11
{
    /// <summary>
    /// A read write structured buffer
    /// </summary>
    public struct RWStructuredBuffer
    {
        public int bufferSize;
        public Buffer buffer;
        public UnorderedAccessView uav;
        public RWStructuredBuffer(int bufferSize, Buffer buffer, UnorderedAccessView uav)
        {
            this.bufferSize = bufferSize;
            this.buffer = buffer;
            this.uav = uav;
        }
    }
}
