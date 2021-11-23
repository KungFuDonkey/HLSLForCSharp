using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct UniformVariableSettings
    {
        public BufferSettings BufferSettings;
        public SRVSettings SRVSettings;

        public UniformVariableSettings(BufferSettings bufferSettings, SRVSettings SRVSettings)
        {
            BufferSettings = bufferSettings;
            this.SRVSettings = SRVSettings;
        }

        public static UniformVariableSettings Standard = new UniformVariableSettings(BufferSettings.StructuredBufferStandard, SRVSettings.StructuredBufferStandard);
    }
}
