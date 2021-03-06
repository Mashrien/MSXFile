﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Mash.MSXArchive {
    sealed class ChunkManager {

        private List<Chunk> UsedChunks;
        private Dictionary<VFile, Chunk> FileTable;
        private List<FileInfo> FilesToAdd;
        private List<DirectoryInfo> DirectoriesToAdd;

        /// <summary>
        /// Returns an Info object containing basic information about the archive;
        /// Archive size-on-disk, number of folders, files, chunks, etc
        /// </summary>
        private void ArchiveInfo() { }

        /// <summary>
        /// List the files owned by a particular node
        /// </summary>
        /// <returns>List of VirtualFiles</returns>
        private List<VFile> DirectoryList() { return new List<VFile>(); }

        /// <summary>
        /// Retruns an Info object containing basic information about a file
        /// </summary>
        /// <param name="vfile">The vfile to query</param>
        private void FileInfo(VFile vfile) { }

        /// <summary>
        /// Extract a file from the archive
        /// </summary>
        /// <param name="vfile">File to extract</param>
        /// <param name="destination">Destination folder on physical disk</param>
        private void ExtractFile(VFile vfile, DirectoryInfo destination) { }

        /// <summary>
        /// Fills empty chunks in the archive by moving chunks from the end of the archive
        /// </summary>
        private void CompactArchive() { }

        /// <summary>
        ///  INTERNAL FUNCTION: In the case of the archive expanding beyond the capacity of 
        ///  the current header by allocating an additional chunk to expand the file table
        /// </summary>
        private void ExpandHeader() { }

        /// <summary>
        /// INTERNAL FUNCTION: Moves specified chunk to the specified index, the existing
        /// chunk will be moved to the first available empty location, or end of archive
        /// </summary>
        private void RelocateChunk() { }

        /// <summary>
        /// Extracts all files in current directory to the specified location on disk
        /// </summary>
        private void ExtractDirectory() { }

        /// <summary>
        /// Existing archives only: Adds a file to the archive immediately, bypassing the queue
        /// For new archives, use the <function>AddFile()</function> function instead
        /// </summary>
        private void AddFileImmediate() { }

        /// <summary>
        /// Existing archives only: Adds a directory and all files within, recursively, to the 
        /// archive immediately, bypassing the queue
        /// For new archives, use the <function>AddDirectory()</function> function instead
        /// </summary>
        private void AddDirectoryImmediate() { }

        /// <summary>
        /// Adds a file to the queue of files to be added to the archive
        /// </summary>
        private void AddFile() { }

        /// <summary>
        /// Adds a directory and all files within, recusrively, to the queue of files to be added
        /// </summary>
        private void AddDirectory() { }

        /// <summary>
        /// Removes a file from the archive
        /// </summary>
        private void RemoveFile() { }
        /// <summary>
        /// Removes a directory and all files within, recursively, from the archive
        /// </summary>
        private void RemoveDirectory() { }

        private void RemoveQueuedFile() { }
        private void RemoveQueuedDirectory() { }
        /// <summary>
        /// Processes the file addition queue to build the archive on disk, raising the <event>ProgressReport()</event>
        /// event periodically (update rate controlled by Configuration.ProgressReportFrequency)
        /// </summary>
        private void BuildArchive() { }

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
            public short CD_Flags;

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
