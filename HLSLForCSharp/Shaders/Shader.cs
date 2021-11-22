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
    public abstract class Shader : IDisposable
    {
        protected static Shader stagedShader;
        protected Device device;

        protected Dictionary<int, RWStructuredBuffer> RWStructuredBuffers = new Dictionary<int, RWStructuredBuffer>();
        protected Dictionary<int, StructuredBuffer> StructuredBuffers = new Dictionary<int, StructuredBuffer>();
        protected List<int> RWSlots = new List<int>();
        protected List<int> StructuredSlots = new List<int>();

        protected DeviceContext context;
        protected Shader(Device device)
        {
            this.device = device;
        }

        #region Buffers
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

        public T[] GetParsedRWBuffer<T>(int slot) where T : struct
        {
            RWStructuredBuffer computeBuffer = RWStructuredBuffers[slot];
            context.MapSubresource(computeBuffer.buffer, MapMode.Read, MapFlags.None, out DataStream ds);
            return ds.ReadRange<T>(computeBuffer.bufferSize);
        }

        #region RWStructuredBuffer
        public void SetRWStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot) where Tin : struct
        {
            SetRWStructuredBuffer<Tin>(bufferData, elementSize, slot, RWStructuredBufferSettings.Standard);
        }

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

        public RWStructuredBuffer GetRWStructuredBuffer(int slot)
        {
            return RWStructuredBuffers[slot];
        }
        #endregion

        #region StructuredBuffers
        public void SetStructuredBuffer<Tin>(Tin[] bufferData, int elementSize, int slot) where Tin : struct
        {
            SetStructuredBuffer<Tin>(bufferData, elementSize, slot, StructuredBufferSettings.Standard);
        }

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

        public StructuredBuffer GetStructuredBuffer(int slot)
        {
            return StructuredBuffers[slot];
        }
        #endregion

        #endregion

        #region Staging
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

        internal abstract void SetStage();

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
