using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mash.MSXArchive {
    /// <summary>
    /// Main entry point and access to the MSX/ION file format handler
    /// </summary>
    public partial class MSX {
        #region Properties
        public ReturnCode ErrorStatus { get; private set; }
        private UInt32 _chunkRangeStart, _chunkRangeEnd; // in cases of split archives, we need to know quickly which chunks each archive-file contains
        private Configuration _config;
        private List<ulong> _chunksProcessing;
        private Dictionary<FileInfo, FileQueueItem> _fileAddQueue;

        private Guid ArchiveGuid;

        public UInt32 ChunkRangeStart {
            get => _chunkRangeStart;
            }
        public UInt32 ChunkRangeEnd {
            get => _chunkRangeEnd;
            }
        public Configuration Options => _config;

        #endregion Properties

        #region Constructor
        /// <summary>
        /// Primary constructor and initializer for MSX- Use new().Open for existing archives
        /// </summary>
        public MSX() {
            _config = new Configuration();
            _chunksProcessing = new List<ulong>();
            _fileAddQueue = new Dictionary<FileInfo, FileQueueItem>();
            
            }
        #endregion Constructor

        #region ArchiveMethods
        /// <summary>
        /// Load an MSX object from a file on disk
        /// Ideally called as; MSX msxFile = (new MSX()).Open(FileInfo);
        /// </summary>
        /// <returns>MSX object</returns>
        public MSX Open() {

            if (FileOpened != null) {
                FileOpenedEventArgs args = new FileOpenedEventArgs() { ErrorLevel = ReturnCode.None, ErrorMessage = string.Empty, ExceptionText = string.Empty };
                FileOpened(this, args);
                if (args.Cancel) {
                    this.ErrorStatus = ReturnCode.ParseFailed;
                    }
                }

            return this;
            }

        public ReturnCode AddFileQueued(FileInfo fi, Guid parent = default(Guid)) {
            if (!_fileAddQueue.ContainsKey(fi)) {
                // try to get the root node if `parent` is empty
                if (parent == default(Guid)) {
                    VFile vf = GetRootVFile();
                    if (vf != null) {
                        parent = vf.FileID;
                        }
                    }
                
                _fileAddQueue.Add(fi, new FileQueueItem(fi, parent));
                }
            return ReturnCode.Success;
            }

        // todo this is the IDEAL method of interacting with the app.. Drag shit in, drag shit out
        // no buttons and as minimal a UI as possible
        public Dictionary<string, ReturnCode> AddFileDropList(DragEventArgs d) {
            Dictionary<string, ReturnCode> retVals = new Dictionary<string, ReturnCode>();

            return retVals;
            }

        public Task<ReturnCode> AddFileImmediate(FileInfo fi) {
            return Task.FromResult<ReturnCode>(ReturnCode.Success);
            }

        public Task<ReturnCode> AddFile(FileInfo fi) {

            return ReturnCodeT(ReturnCode.Success);
            }

        public ReturnCode RemoveFileFromAddQueue(Guid file) {

            return ReturnCode.Success;
            }

        public ReturnCode DeleteFileFromArchive(Guid file) {
            return ReturnCode.Success;
            }


        private VFile GetRootVFile() {
            // todo GetRootNodeGuid - walk archive to get root node
            // though, realistically, Guid.empty will most likey be the root node as 
            // having a static guid application-wide would probably make things a LOT
            // easier down the line, not just programmatically, but also for my brain
            // in trying to recall all this shit
            return new VFile(default(Guid), default(Guid));
            }

        /// <summary>
        /// Begins processing the file addition queue
        /// </summary>
        /// <param name="callback">Expects a callback (or null) with the parameters: byteMin, byteMax, byteComplete, string FileName</param>
        /// <returns>BOOL, set TRUE to cancel the operation</returns>
        public ReturnCode ProcessAddQueue(Func<int, int, int, string, bool> callback) {

            return ReturnCode.Success;
            }
        public ReturnCode ProcessAddQueue() {

            return ReturnCode.Success;
            }

        public ReturnCode SaveArchive() {

            return ReturnCode.Success;
            }

        #endregion ArchiveMethods



        }
    }
