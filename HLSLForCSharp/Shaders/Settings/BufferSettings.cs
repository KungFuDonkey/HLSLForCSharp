﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace HLSLForCSharp.Shaders.Settings
{
    public struct BufferSettings
    {
        public ResourceUsage usage;
        public BindFlags bindFlags;
        public ResourceOptionFlags options;
        public CpuAccessFlags CPUAccessFlags;
        public BufferSettings(ResourceUsage usage, BindFlags bindFlags, ResourceOptionFlags options, CpuAccessFlags CPUAccessFlags)
        {
            this.usage = usage;
            this.bindFlags = bindFlags;
            this.options = options;
            this.CPUAccessFlags = CPUAccessFlags;
        }

        public static BufferSettings RWBufferStandard = new BufferSettings(ResourceUsage.Default, BindFlags.UnorderedAccess, ResourceOptionFlags.BufferStructured, CpuAccessFlags.Read);
    }
}
