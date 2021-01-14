using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    public class VFile {
        private Guid _fileGuid, _ownerGuid; // 128bit / 32byte // stored as 2x long
        private UInt32 _numOfChunks; // 4byte
        private DateTime _cDate, _mDate, _aDate; // stored as 2 longs representing epoch time
        private FileFlags _fflags; // short
        private HeaderFlags _hflags; // short
        private string _name; // 128byte null-terminated fixed-length string
        private UInt64 _uSize, _cSize; // 2x 8byte ints

        /// <summary>
        /// Indexed list of chunks of the vfile as filePieceNumber--archiveChunkIndex and should be used as:
        /// for (0 .. vFile.OrderedChunks.Length) Archive.GetFileChunk(vFile, OrderedChunks[index])
        /// eg; The 0th index could point to chunk number 418, or byte 1,712,128 in the archive
        /// </summary>
        public Dictionary<UInt32, UInt32> OrderedChunks {
            get; private set;
            }

        public FileFlags Flags { get => _fflags; }

        public string Name { get => _name; }

        /// <summary>
        /// Tuple of longs representing the UUID of the file; (long1, long2) = uuid
        /// </summary>
        public (long a, long b) UUIDAsLongs {
            get {
                return _fileGuid.ToLongs();
                }
            set {
                Guid parsed = default(Guid);
                //pbug guidfromlongs.tostring.tochararray likely to fail
                if (Guid.TryParse(GuidTools.GuidFromLongs(value.a, value.b).ToString().ToCharArray(), out parsed)) {
                    _fileGuid = parsed;
                    } // else no assignment
                }
            }

        /// <summary>
        /// GUID of this vFile
        /// </summary>
        public Guid FileID {
            get => _fileGuid;
            }

        /// <summary>
        /// GUID of this vFile's parent node (0 if it's in the root dir)
        /// </summary>
        public Guid OwnerID {
            get => _ownerGuid;
            }

        /// <summary>
        /// Size of the original file on disk, uncompressed
        /// </summary>
        public UInt64 SizeOriginal {
            get => _uSize;
            }



        public VFile(Guid fileGuid, Guid parentNode) {
            OrderedChunks = new Dictionary<UInt32, UInt32>();
            _fileGuid = fileGuid;
            _ownerGuid = parentNode;
            }


        }
    }


/*todo how in the hell ..
 * How are we going to create a storable list of shit to recurse?
 * typical recursive-func we spam-call, minding the insertion order,
 * and just slap that into a flat List<>?- But then how to handle
 * ownership and inheritance?.. Handle that by lookups upon the
 * actual file processing step? Would like to create the archive as
 * the final step- "Save" will prompt for a name and location, THEN
 * we begin actually pulling the files from the system and starting
 * the process of chunking down the files and pushing them into the
 * archive..
 * ALSO; Going to have to figure out how to handle empty chunks in
 * the middle of the archive, so we can have a "solid" archive even
 * in the event files are deleted down-the-line.
 * 
 * The ENTIRE point of this overly-complex system is to avoid, at
 * ALL cost, ever re-writing massive portions of the archive or ever
 * needing to pull more than a handful of chunks into memory at any
 * one time.. So this will be an IDEAL network/internet long-term
 * storage and archival tool. Even with 100gb backups stored on the
 * network, pulling a 20mb file from the archive means we only ever
 * need to fetch the 20mb file + 1-2 chunks of 4096-bytes/each worth
 * of data over the network.
 * 
 * 
 * ArchiveFlags += EmptyChunks means we find a file containing as
 * close to the number of empty chunks possible and then relocate
 * that file's chunks into the empty ones.. then trimming off the
 * end of the file, so that the archive CAN shrink in size-on-disk
 * when files are deleted from the archive--Again, without ever
 * rebuilding the archive, or doing large amounts of data relocation
 * 
 * Hopefully.  Get the feeling this is going to be a multi-month
 * project.. at the least
 */