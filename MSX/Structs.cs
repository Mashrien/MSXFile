using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    sealed class Structs {
        /// 
        /// Because of the sheer frequency at which we'll be reading headers/chunks into these
        /// data structures, we're doing this unmanaged and will be reading the bytes DIRECTLY
        /// into the structs, rather than doing a manual assignment or interation of any kind
        /// as I consider it fairly unlikely anyone will be pulling single small files from the
        /// archive and will more realistically be pulling many large files from it (video etc)
        /// necessitating the marshaling of potentially thousands of chunks as-fast-as-possible
        /// 
        /// and this is rather low-hanging fruit as far as speed-optimization goes, as well as
        /// our memory footprint. I'm not going to be obsessive about memory utilization, this
        /// IS C# after all, but I am still rather conscious of it. Keeping mem usage down to 
        /// 100-200mb or so when dealing with dozens-of-gigabyte archives seems perfectly
        /// reasonable to me
        ///

        /* Struct- Archive Header (4096bytes, 512 info, 3584 author notes or null)
         * Magic Number
         * File format version
         * File alignment (1k, 2k, 4k, 8k, 16k, 32k, 64k)
         * Archive flags
         * Guid of archive - root node
         * Original archive-file name
         * Number of chunks in archive
         * Number of files in archive
         * 3DES encrypted copy of GUID (used to verify decryption)
         * SHA256 of archive password
         * Hash of header (hash field filled with 0s for hashing algorithm)
         */

        [StructLayout(LayoutKind.Sequential)] // LayoutKind.Explicit may be required
        struct ArchiveHeader {
            //[FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string CD_MAGICNUMBER;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string CD_FileFormatVer; // this is actually 4x numbers 0-16, stored in a short

            //[FieldOffset(4)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string CD_UUID_Archive;

            //[FieldOffset(20)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string CD_UUID_RootNode;

            //[FieldOffset(36)]
            [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
            public FileFlags CD_Flags;

            [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
            public Attribs CD_Attributes;

            //[FieldOffset(38)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] // Tools.FixedLengthStringEncoding.Encode/.Decode
            public string CD_OriginalFileName;

            ////[FieldOffset(102)]
            //[MarshalAs(UnmanagedType.U8, SizeConst = 8)]
            //public UInt64 CD_CompSize;

            ////[FieldOffset(110)]
            //[MarshalAs(UnmanagedType.U8, SizeConst = 8)]
            //public UInt64 CD_UnCompSize;

            //[FieldOffset(118)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] // hash of all 1st chunks (hash of headers)
            public string CD_CompHash;

            //[FieldOffset(182)]
            [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
            public UInt32 CD_ArchiveChunks;

            //[FieldOffset(186)]
            [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
            public UInt16 CD_PtrFirstChunk;

            //[FieldOffset(188)]
            //[MarshalAs(UnmanagedType.U2, SizeConst = 2)]
            //public UInt16 CD_PtrFirstChunk;



            }


        }
    }
