using System;
using System.Collections.Generic;
using System.Text;
using static Mash.MSXArchive.MSX;

namespace Mash.MSXArchive {
    public partial class MSX {

        public event EventHandler<FileOpenedEventArgs> FileOpened;
        public event EventHandler<OperationProgressArgs> ProgressReport;

        }

    /// <summary>
    /// Event raised when file open process completes
    /// Set event.Cancel on the object to abort if ErrorStatus is undesirable
    /// </summary>
    public class FileOpenedEventArgs {
        public ReturnCode ErrorLevel;
        public string ErrorMessage;
        public string ExceptionText;
        public bool Cancel = false;
        }

    /// <summary>
    /// Event fires as any file operation progresses and is configurable with <MSX.MSXArchive.Options>Options</MSX.MSXArchive.Options>
    /// Values contained are as below
    /// <Mash.MSXArchive.MSX.ErrorLevels>ErrorLevel</Mash.MSXArchive.MSX.ErrorLevels>
    /// <string>FileName</string> containing relevant file or archive name
    /// <float>Progress</float> operation progress from 0.0000 to 1.0000 (rounded to 4 decimal places)
    /// <long>BytesMin</long>, <long>BytesMax</long>, and the current <bytes>BytesComplete</bytes>
    /// </summary>
    public class OperationProgressArgs {
        public ReturnCode ErrorLevel;
        public string FileName;
        public float Progress;
        public long BytesMin, BytesMax, BytesComplete;
        }

    }
