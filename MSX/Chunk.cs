using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mash.MSXArchive {
    sealed class Chunk {
        private int chunkNumber;
        private bool firstChunk; // first chunk contains file info - 512bytes - subsequent chunks are 128bytes
        private Guid objectOwner; // file this chunk belongs to

        public Chunk(int chunkNumber, bool isFirstChunk, Guid ownerGuid) {
            this.chunkNumber = chunkNumber;
            this.firstChunk = isFirstChunk;
            this.objectOwner = ownerGuid;
            }

        // this won't work, need to maintain 4k alignment at all costs, for future file>del > defrag/compact funcs to work
        public byte[] GetBytes(Stream msxStream, IOReader ior) {
            int bytesToRead = firstChunk ? 3584 : 3698;
            // seek the stream to archive.ChunkSize * chunkNumber - 128/512(depending on firstChunk)
            msxStream.Seek((chunkNumber * 4096) + (firstChunk ? 512 : 128), SeekOrigin.Begin);
            byte[] retv = ior.ReadBytes(bytesToRead);

            return new byte[] { };
            }

        }
    }
