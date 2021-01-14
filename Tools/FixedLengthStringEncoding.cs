using System;
using System.Collections.Generic;
using System.Text;

namespace Mash.MSXArchive.Tools {
    static class FixedLengthStringEncoding {

        static public string Encode(string source, int length) {
            byte[] bytes = Encoding.UTF8.GetBytes(source);

            StringBuilder buffer = new StringBuilder(length);
            buffer.Append(System.Convert.ToBase64String(bytes));
            while (buffer.Length < length) {
                buffer.Append('=');
                }
            return buffer.ToString();
            }

        static public string Decode(string encoded) {
            int index = encoded.IndexOf('=');
            if (index > 0) {
                encoded = encoded.Substring(0, ((index + 3) / 4) * 4);
                }
            byte[] bytes = System.Convert.FromBase64String(encoded);
            return System.Text.Encoding.UTF8.GetString(bytes);
            }

        }
    }
