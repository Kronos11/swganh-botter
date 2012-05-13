using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGANH.Core
{
    public class MessageHelper
    {
        //public static void CopyStream(Stream input, Stream output)
        //{
        //    byte[] buffer = new byte[32768];
        //    while (true)
        //    {
        //        int read = input.Read(buffer, 0, buffer.Length);
        //        if (read <= 0) return;
        //        output.Write(buffer, 0, read);
        //    }
        //}
        public static byte[] Compress(byte[] data)
        {
            try
            {
                using (var compressedStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                    {

                        zipStream.Write(data, 0, data.Length);
                        zipStream.Close();

                        return compressedStream.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            try
            {
                using (var compressedStream = new MemoryStream(data))
                {
                    using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                    {
                        using (var resultStream = new MemoryStream())
                        {
                            zipStream.CopyTo(resultStream);

                            return resultStream.ToArray();
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
