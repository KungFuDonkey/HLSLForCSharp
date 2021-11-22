using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using HLSLForCSharp.Shaders.Settings;

namespace HLSLForCSharp.Shaders
{
    public class ComputeShader : Shader, IDisposable
    {
        public int groupSize { get; private set; }
        SharpDX.Direct3D11.ComputeShader shader;

        public ComputeShader(string shaderPath, int groupSize) : this(DeviceManager.device, shaderPath, groupSize, ComputeShaderSettings.Standard) { }

        public ComputeShader(string shaderPath, int groupSize, ComputeShaderSettings settings) : this(DeviceManager.device, shaderPath, groupSize, settings) { }

        public ComputeShader(Device device, string shaderPath, int groupSize) : this(device, shaderPath, groupSize, ComputeShaderSettings.Standard) { }
        
        public ComputeShader(Device device, string shaderPath, int groupSize, ComputeShaderSettings settings)
            : base(device)
        {
            this.groupSize = groupSize;
            CompilationResult bytecode = ShaderBytecode.CompileFromFile(shaderPath, settings.entryPoint, settings.profile, settings.shaderFlags, settings.effectFlags, settings.shaderMacros, settings.include); 
            this.shader = new SharpDX.Direct3D11.ComputeShader(device, bytecode);
            bytecode.Dispose();
        }

        #region Staging
        internal override void SetStage()
        {
            //Set up shader
            context = device.ImmediateContext;
            context.ComputeShader.Set(shader);
            
        }

        public override void UnStage()
        {
            context.ClearState();
            base.UnStage();
        }
        #endregion

        #region ShaderSpecificFunctions
        protected override void SetConstantBuffer(int slot, SharpDX.Direct3D11.Buffer buffer)
        {
            context.ComputeShader.SetConstantBuffer(slot, buffer);
        }

        protected override void SetShaderResource(int slot, ShaderResourceView srv)
        {
            context.ComputeShader.SetShaderResource(slot, srv);
        }

        protected override void SetUnorderedAccessView(int slot, UnorderedAccessView uav)
        {
            context.ComputeShader.SetUnorderedAccessView(slot, uav);
        }

        protected override void SetSampler(int slot, SamplerState sampler)
        {
            context.ComputeShader.SetSampler(slot, sampler);
        }
        #endregion

        public void DispatchCompute(int x, int y, int z)
        {
            context.Dispatch(x, y, z);
        }

        public override void Dispose()
        {
            base.Dispose();
            Utilities.Dispose(ref shader);
        }
    }
}
