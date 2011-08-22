﻿/* Copyright (c) 2011 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Diagnostics;
using System.IO;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        public static UInt32 ReadValueU32(this Stream stream)
        {
            return stream.ReadValueU32(true);
        }

        public static UInt32 ReadValueU32(this Stream stream, bool littleEndian)
        {
            byte[] data = new byte[4];
            int read = stream.Read(data, 0, 4);
            Debug.Assert(read == 4);
            UInt32 value = BitConverter.ToUInt32(data, 0);

            if (ShouldSwap(littleEndian))
            {
                value = value.Swap();
            }

            return value;
        }

        public static void WriteValueU32(this Stream stream, UInt32 value)
        {
            stream.WriteValueU32(value, true);
        }

        public static void WriteValueU32(this Stream stream, UInt32 value, bool littleEndian)
        {
            if (ShouldSwap(littleEndian))
            {
                value = value.Swap();
            }

            byte[] data = BitConverter.GetBytes(value);
            Debug.Assert(data.Length == 4);
            stream.Write(data, 0, 4);
        }
    }
}
