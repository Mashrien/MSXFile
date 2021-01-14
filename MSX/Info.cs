using System;
using System.Collections.Generic;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    sealed class Info {
        public string FileName;
        public string ArchiveName;
        public DateTime CreationDate;
        public DateTime ModificationDate;
        public DateTime AccessDate;
        public Attribs Attributes;
        }
    }
