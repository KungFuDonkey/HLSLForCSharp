using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLSLForCSharp.Direct3D11.Settings
{
    public struct StructuredBufferSettings 
    {
        public SRVSettings SRVSettings;
        public BufferSettings BufferSettings;

        public StructuredBufferSettings(SRVSettings SRVSettings, BufferSettings BufferSettings)
        {
            this.SRVSettings = SRVSettings;
            this.BufferSettings = BufferSettings;
        }

        public static StructuredBufferSettings Standard = new StructuredBufferSettings(SRVSettings.StructuredBufferStandard, BufferSettings.RWBufferStandard);
    }
}
