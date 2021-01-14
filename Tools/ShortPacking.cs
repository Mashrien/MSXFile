using System;
using System.Collections.Generic;
using System.Text;

namespace Mash.MSXArchive.Tools {
    static class ShortPacking {
        public static (byte, byte, byte, byte) UnpackShort(short val) {
            byte a = (byte)((val >> 12) & 0xf);
            byte b = (byte)((val >> 8) & 0xf);
            byte c = (byte)((val >> 4) & 0xf);
            byte d = (byte)(val & 0xF);
            return (a, b, c, d);
            }

        public static short PackShort(byte a, byte b, byte c, byte d) {
            return (short)((a << 12) | (b << 8) | (c << 4) | d);
            }

        }
    }
