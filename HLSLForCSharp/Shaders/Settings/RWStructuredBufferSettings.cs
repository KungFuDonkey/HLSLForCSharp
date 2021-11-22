using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLSLForCSharp.Shaders.Settings
{
    public struct RWStructuredBufferSettings
    {
        public UAVSettings UAVSettings;
        public BufferSettings BufferSettings;

        public RWStructuredBufferSettings(UAVSettings UAVSettings, BufferSettings BufferSettings)
        {
            this.UAVSettings = UAVSettings;
            this.BufferSettings = BufferSettings;
        }

        public static RWStructuredBufferSettings Standard = new RWStructuredBufferSettings(UAVSettings.RWBufferStandard, BufferSettings.RWBufferStandard);
    }
}
