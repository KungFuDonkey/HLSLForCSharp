using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct ComputeShaderSettings
    {
        public string entryPoint;
        public string profile;
        public ShaderFlags shaderFlags;
        public EffectFlags effectFlags;
        public ShaderMacro[] shaderMacros;
        public Include include;
        public ComputeShaderSettings(string entryPoint, string profile, ShaderFlags shaderFlags, EffectFlags effectFlags, ShaderMacro[] shaderMacros, Include include)
        {
            this.entryPoint = entryPoint;
            this.profile = profile;
            this.shaderFlags = shaderFlags;
            this.effectFlags = effectFlags;
            this.shaderMacros = shaderMacros;
            this.include = include;
        }

        public static ComputeShaderSettings Standard = new ComputeShaderSettings("CSMain", "cs_5_0", ShaderFlags.None, EffectFlags.None, null, null);
    }
}
