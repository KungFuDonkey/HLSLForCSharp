using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using SharpDX.Multimedia;
using HLSLForCSharp.Shaders.Settings;

namespace HLSLForCSharp.Shaders
{
    /// <summary>
    /// Shader for an HLSL shader
    /// </summary>
    public abstract class Shader : IDisposable
    {
        /// <summary>
        /// The shader that is currently in use
        /// </summary>
        protected static Shader stagedShader;

        /// <summary>
        /// The device that this shader is on
        /// </summary>
        protected Device device;

        /// <summary>
        /// The context of the device
        /// </summary>
        protected DeviceContext context;

        /// <summary>
        /// The buffers of the shader, with their given slots
        /// </summary>
        protected Dictionary<int, RWStructuredBuffer> RWStructuredBuffers = new Dictionary<int, RWStructuredBuffer>();
        protected Dictionary<int, StructuredBuffer> StructuredBuffers = new Dictionary<int, StructuredBuffer>();
        protected List<int> RWSlots = new List<int>();
        protected List<int> StructuredSlots = new List<int>();

        
        protected Shader(Device device)
        {
            this.device = device;
        }

        #region Buffers

        /// <summary>
        /// Creates a DirectX buffer
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="bufferData"></param>
        /// <param name="elementSize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private SharpDX.Direct3D11.Buffer CreateBuffer<Tin>(Tin[] bufferData, int elementSize, BufferSettings settings) where Tin : struct
        {
            BufferDescription inputDesc = new BufferDescription()
            {
                SizeInBytes = elementSize * bufferData.Length,
                Usage = settings.usage,
                BindFlags = settings.bindFlags,
                OptionFlags = settings.options,
                StructureByteStride = elementSize,
                CpuAccessFlags = settings.CPUAccessFlags
            };
            SharpDX.Direct3D11.Buffer buffer = SharpDX.Direct3D11.Buffer.Create(device, bufferData, inputDesc);
            return buffer;
        }

        /// <summary>
        /// Retrieves a read-write buffer from the GPU
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slot"></param>
        /// <returns></returns>
        public T[] GetParsedRWBuffer<T>(int slot) where T : struct
        {
            RWStructuredBuffer computeBuffer = RWStructuredBuffers[slot];
            context.MapSubresource(computeBuffer.buffer, MapMode.Read, MapFlags.None, out DataStream ds);
            return ds.ReadRange<T>(computeBuffer.bufferSize);
        }

        #region RWStructuredBuffer

        /// <summary>
        /// Sets a read-write buffer on the GPU
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="bufferData"></param>
        /// <param name="elementSize"></param>
        /// <param name="slot"></param>
        public void SetRWStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot) where Tin : struct
        {
            SetRWStructuredBuffer<Tin>(bufferData, elementSize, slot, RWStructuredBufferSettings.Standard);
        }

        /// <summary>
        /// Sets a read-write buffer on the GPU with settings
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="bufferData"></param>
        /// <param name="elementSize"></param>
        /// <param name="slot"></param>
        /// <param name="settings"></param>
        public void SetRWStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot, RWStructuredBufferSettings settings) where Tin : struct
        {
            if (stagedShader != this) throw new Exception("Shader was not staged");
            SharpDX.Direct3D11.Buffer buffer = CreateBuffer(bufferData, elementSize, settings.BufferSettings);

            UnorderedAccessViewDescription uavDesc = new UnorderedAccessViewDescription()
            {
                Format = settings.UAVSettings.format,
                Dimension = settings.UAVSettings.dimension,
                Buffer = new UnorderedAccessViewDescription.BufferResource()
                {
                    ElementCount = bufferData.Length
                }
            };
            UnorderedAccessView uav = new UnorderedAccessView(device, buffer, uavDesc);

            //Set up shader's buffer
            SetConstantBuffer(slot, buffer);
            SetUnorderedAccessView(slot, uav);

            if (RWStructuredBuffers.ContainsKey(slot))
            {
                RWStructuredBuffers[slot] = new RWStructuredBuffer(bufferData.Length, buffer, uav);
                return;
            }
            RWStructuredBuffers.Add(slot, new RWStructuredBuffer(bufferData.Length, buffer, uav));
            RWSlots.Add(slot);
        }

        /// <summary>
        /// Gets the RWStructured buffer struct
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public RWStructuredBuffer GetRWStructuredBuffer(int slot)
        {
            return RWStructuredBuffers[slot];
        }
        #endregion

        #region StructuredBuffers

        /// <summary>
        /// Sets a stuctured buffer on the GPU
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="bufferData"></param>
        /// <param name="elementSize"></param>
        /// <param name="slot"></param>
        public void SetStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot) where Tin : struct
        {
            SetStructuredBuffer<Tin>(bufferData, elementSize, slot, StructuredBufferSettings.Standard);
        }

        /// <summary>
        /// Sets a stuctured buffer on the GPU with settings
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="bufferData"></param>
        /// <param name="elementSize"></param>
        /// <param name="slot"></param>
        /// <param name="settings"></param>
        public void SetStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot, StructuredBufferSettings settings) where Tin : struct
        {
            if (stagedShader != this) throw new Exception("Shader was not staged");
            SharpDX.Direct3D11.Buffer buffer = CreateBuffer(bufferData, elementSize, settings.BufferSettings);


            ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription()
            {
                Format = settings.SRVSettings.format,
                Dimension = settings.SRVSettings.dimension,
                Buffer = new ShaderResourceViewDescription.BufferResource()
                {
                    ElementWidth = elementSize
                }
            };
            ShaderResourceView srv = new ShaderResourceView(device, buffer, srvDesc);

            SetConstantBuffer(slot, buffer);
            SetShaderResource(slot, srv);

            if (RWStructuredBuffers.ContainsKey(slot))
            {
                StructuredBuffers[slot] = new StructuredBuffer(bufferData.Length, buffer, srv);
                return;
            }
            StructuredBuffers.Add(slot, new StructuredBuffer(bufferData.Length, buffer, srv));
            StructuredSlots.Add(slot);
        }

        /// <summary>
        /// Gets the structuredBuffer struct
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public StructuredBuffer GetStructuredBuffer(int slot)
        {
            return StructuredBuffers[slot];
        }
        #endregion

        #endregion

        #region Staging

        /// <summary>
        /// Stages the shader for use on the GPU
        /// </summary>
        public void Stage()
        {
            if (stagedShader != null)
            {
                Debug.WriteLine("**WARNING** previous shader was not unstaged");
                Console.WriteLine("**WARNING** previous shader was not unstaged");
            }
            stagedShader = this;
            SetStage();
        }

        /// <summary>
        /// Abstract as all different shaders stage differently
        /// </summary>
        protected abstract void SetStage();

        /// <summary>
        /// Unstages the shader and disposes all uavs and srvs
        /// </summary>
        public virtual void UnStage()
        {
            for (int i = RWSlots.Count - 1; i >= 0; i--)
            {
                RWStructuredBuffer computeBuffer = RWStructuredBuffers[RWSlots[i]];
                RWStructuredBuffers.Remove(RWSlots[i]);
                RWSlots.RemoveAt(i);
                Utilities.Dispose(ref computeBuffer.uav);
                Utilities.Dispose(ref computeBuffer.buffer);
            }

            for (int i = StructuredSlots.Count - 1; i >= 0; i--)
            {
                StructuredBuffer buffer = StructuredBuffers[StructuredSlots[i]];
                RWStructuredBuffers.Remove(RWSlots[i]);
                RWSlots.RemoveAt(i);
                Utilities.Dispose(ref buffer.srv);
                Utilities.Dispose(ref buffer.buffer);
            }
            stagedShader = null;
        }

        #endregion

        #region ShaderSpecificFunctions

        protected abstract void SetConstantBuffer(int slot, SharpDX.Direct3D11.Buffer buffer);

        protected abstract void SetShaderResource(int slot, ShaderResourceView srv);

        protected abstract void SetUnorderedAccessView(int slot, UnorderedAccessView uav);

        protected abstract void SetSampler(int slot, SamplerState sampler);

        #endregion

        public virtual void Dispose()
        {
            UnStage();
        }
    }
}
