using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    sealed class Manager {

        private List<Chunk> UsedChunks;
        private List<Header> HeaderChunks;
        private uint TotalChunkCount;
        private Dictionary<VFile, List<Chunk>> FileTable;
        private List<FileInfo> FilesToAdd;
        private List<DirectoryInfo> DirectoriesToAdd;

        private FileInfo ArchiveFile;

        /// <summary>
        /// Returns an Info object containing basic information about the archive;
        /// Archive size-on-disk, number of folders, files, chunks, etc
        /// </summary>
        private void ArchiveInfo() { }

        /// <summary>
        /// List the files owned by a particular node
        /// </summary>
        /// <returns><ReturnCode>enum indicating status</ReturnCode></returns>
        private ReturnCode DirectoryList(VFile owner, out List<VFile> vfiles) {
            vfiles = new List<VFile>();
            ReturnCode retc = ReturnCode.None;
            foreach (VFile vf in FileTable.Keys) {
                if (vf.OwnerID == owner.FileID) {
                    vfiles.Add(vf);
                    retc = ReturnCode.Success;
                    }
                }
            return retc;
            }

        /// <summary>
        /// Retruns an Info object containing basic information about a file
        /// </summary>
        /// <param name="vfile">The vfile to query</param>
        private void FileInfo(VFile vfile) { }


        public Chunk GetChunk(VFile vfile, int chunkNumber) {
            //foreach (Chunk c in FileTable[vfile]) {
            //    }
            
            return FileTable[vfile][chunkNumber];

            }

        /// <summary>
        /// Extract a file from the archive
        /// </summary>
        /// <param name="vfile">File to extract</param>
        /// <param name="destination">Destination folder on physical disk</param>
        //private ReturnCode ExtractFile(VFile vfile, DirectoryInfo destination) {
        //    FileStream fs;
        //    try {
        //        fs = File.Create(Path.Combine(destination.FullName, "\\", vfile.Name));
        //        } catch (Exception ex) {
        //        Trace.WriteLine($"Extraction failed: {ex.Message}");
        //        return ReturnCode.OutputAccessDenied | ReturnCode.Permission;
        //        }

        //    for (int i = 1; i < vfile.OrderedChunks.Count; i++) {
                
        //        }
        //    return ReturnCode.Success;
        //    }

        /// <summary>
        /// Fills empty chunks in the archive by moving chunks from the end of the archive
        /// </summary>
        private ReturnCode CompactArchive() {
            if (ArchiveFile?.Length < 4096) return ReturnCode.ArchiveUninitialized;
            // totalChunks = FileLength / 4096
            long TotalChunks = (ArchiveFile.Length / 4096);


            return ReturnCode.Success;
            }

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

        }
    }
