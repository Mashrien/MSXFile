using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//todo flesh out the IO funcs
namespace Mash.MSXArchive {
    sealed class IOWriter : BinaryWriter {
        public IOWriter() {
            
            }

        }

    sealed class IOReader : BinaryReader {
        public IOReader(Stream input) : base(input) {
            }
        }

    }
