namespace HLSLForCSharp.Direct3D11.Settings
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
