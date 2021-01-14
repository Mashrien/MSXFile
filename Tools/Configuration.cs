using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Mash.MSXArchive {
    public class Configuration {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpace(string lpDirectoryName,
            out ulong lpBlocksPerCluster,
            out ulong lpBytesPerBlock,
            out ulong waste1,
            out ulong waste2,
            out ulong waste3
            );


        public int ProgressReportFrequency { get; private set; }
        public bool OverrideByDefault { get; private set; }
        
        /// <summary>
        /// Chunk size used for reading/writing, will be written into archives for safety
        /// in case archives are ever moved to a filesystem that uses a different chunksize
        /// 4096 by default for modern drives / NTFS, but it COULD possibly be different.
        /// The constructor will determine and set this value automatically via syscalls
        /// </summary>
        public short ChunkSize;

        public Configuration() {
            ProgressReportFrequency = 250;
            OverrideByDefault = true;
            ulong lpBlocksPerCluster, lpBytesPerBlock;
            Trace.WriteLine("Executing system call ..");
            bool success = GetDiskFreeSpace("C:\\", out lpBlocksPerCluster, out lpBytesPerBlock,
                   out _, out _, out _);
            if (!success) {
                MessageBox.Show($"Failed to read filesystem cluster size via p/invoke call.{Environment.NewLine}Program will throw an exception and exit.");
                throw new System.ComponentModel.Win32Exception();
                Environment.Exit(-1);
                }

            ChunkSize = (short)(lpBlocksPerCluster * lpBytesPerBlock);
            Trace.WriteLine($"Config > File system cluster size: {ChunkSize}");

            }

        public void ChangeSettings(int reportFreq, bool overrideFiles, short chunkSize) {
            ProgressReportFrequency = reportFreq;
            OverrideByDefault = overrideFiles;
            ChunkSize = chunkSize;
            }

        }
    }
