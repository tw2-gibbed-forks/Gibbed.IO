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
        public static Double ReadValueF64(this Stream stream)
        {
            return stream.ReadValueF64(true);
        }

        public static Double ReadValueF64(this Stream stream, bool littleEndian)
        {
            byte[] data = new byte[8];
            int read = stream.Read(data, 0, 8);
            Debug.Assert(read == 8);

            if (ShouldSwap(littleEndian))
            {
                return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(data, 0).Swap());
            }
            else
            {
                return BitConverter.ToDouble(data, 0);
            }
        }

        public static void WriteValueF64(this Stream stream, Double value)
        {
            stream.WriteValueF64(value, true);
        }

        public static void WriteValueF64(this Stream stream, Double value, bool littleEndian)
        {
            byte[] data;
            if (ShouldSwap(littleEndian))
            {
                data = BitConverter.GetBytes(BitConverter.DoubleToInt64Bits(value).Swap());
            }
            else
            {
                data = BitConverter.GetBytes(value);
            }
            Debug.Assert(data.Length == 8);
            stream.Write(data, 0, 8);
        }
    }
}
