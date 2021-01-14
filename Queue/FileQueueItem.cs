using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    internal class FileQueueItem {
        public Guid ID { get; private set; }
        public Guid Parent { get; private set; }
        private string _name;
        private string _path;
        public string Name {
            get {
                if (_name.Length > 127) {
                    return _name.Substring(0, 127);
                    } else {
                    return _name;
                    }
                }
            set {
                if (value?.Length > 127) {
                    _name = value.Substring(0, 127);
                    } else {
                    _name = value;
                    }
                }
            }
        public string FullPath {
            get => Path.GetFullPath(Path.Combine(_path, _name));
            }
        public (ulong, ulong) IDLongs {
            get => ID.ToULongs();
            set => ID = GuidTools.GuidFromULongs(value.Item1, value.Item2);
            }
        public (ulong, ulong) ParentLongs {
            get => Parent.ToULongs();
            set => Parent = GuidTools.GuidFromULongs(value.Item1, value.Item2);
            }

        public FileQueueItem() {
            throw new NotImplementedException("Cannot create an empty FileQueueItem object");
            }

        public ReturnCode ChangeOwnerGuid(Guid parent) {
            Parent = parent;
            return ReturnCode.Success;
            }

        public FileQueueItem(FileInfo fi, Guid parent) {
            ID = Guid.NewGuid();
            Parent = parent;
            _path = fi.DirectoryName;
            _name = fi.Name;
            }
        }
    }
