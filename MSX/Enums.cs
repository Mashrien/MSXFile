using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mash.MSXArchive {
    public partial class MSX {

        /// <summary>
        /// Error level enum for return values of internal functions
        /// </summary>
        public enum ReturnCode : ulong {
            Success                 = 1,
            Failed                  = 2,
            None                    = 4,
            Unknown                 = 8,
            Info                    = 16,
            Warning                 = 32,
            Depreciated             = 64,
            GenericIO               = 128,
            Permission              = 256,
            CyclingRedundancy       = 512,
            InvalidHash             = 1024,
            HashMismatch            = 2048,
            PasswordInvalid         = 4096,
            ChunkAlignment          = 8192,
            HeaderHashMismatch      = 16384,
            HeaderCorrupt           = 32768,
            ParseFailed             = 65536,
            NotValidMSXFile         = 131072,
            UnexpectedEndOfFile     = 262144,
            ArchiveIncomplete       = 524288,
            MissingChunk            = 1048576,
            OrphanedChunk           = 2097152,
            GuidNotFound            = 4194304,
            ChunkAllocFailed        = 8388608,
            ChunkMoveFailed         = 16777216,
            ChildRebaseFailed       = 33554432,
            PInvokeFailed           = 67108864,
            Win32APICallFailed      = 134217728,
            ArchiveTrimFailed       = 268435456,
            ArchiveInvalid          = 536870912,
            ArchiveNonExistant      = 1073741824,
            ArchiveUninitialized    = 2147483648,
            ArchiveLengthInvalid    = 4294967296,
            OutputAccessDenied      = 8589934592,
            }

        public Task<ReturnCode> ReturnCodeT(ReturnCode rc) {
            return Task.FromResult<ReturnCode>(rc);
            }

        /// <summary>
        /// Archive flags, packed as short (2-byte) for alignment
        /// </summary>
        public enum ArchiveFlags : short {
            None = 0x0, // should exclude all other flags
            Compressed = 1 << 0,
            Encrypted = 1 << 1,
            Multipart = 1 << 2,
            Broken = 1 << 3,
            Corrupted = 1 << 4,
            Incomplete = 1 << 5,
            BadHeaderHash = 1 << 6,
            BadArchiveHash = 1 << 7
            }

        /// <summary>
        /// Chunk header flags, packed as short (2-byte) for alignment
        /// </summary>
        public enum HeaderFlags : short {
            none = 0x0, // exclude all other flags
            Compressed = 1 << 0,
            Encrypted = 1 << 1,
            SplitAcrossFiles = 1 << 2,
            Broken = 1 << 3,
            Corrupted = 1 << 4,
            IsFolder = 1 << 5, // unused -- use FileFlags.IsDirectory
            BadFileHash = 1 << 6
            }

        public enum FileFlags : short {
            none = 0x0,
            SplitAcrossFiles = 1 << 0,
            Broken = 1 << 1,
            Corrupted = 1 << 2,
            IsDirectory = 1 << 3,
            }

        public enum Attribs : short {
            ReadOnly = 1,
            Archive = 2,
            System = 4,
            Hidden = 8
            }

        /// <summary>
        /// File open/save filters
        /// </summary>
        public static Dictionary<string, string> FileTypes = new Dictionary<string, string>() { 
                { "msx", "Mash Seekable Archive files | *.msx" }, 
                { "ion", "Indexable Over Network files | *.ion" }
            };


        }

    /*
     * 
     * 
     */

    }
