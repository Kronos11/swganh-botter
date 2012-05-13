using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWGANH.Core.Tests
{
    [TestClass]
    public class CompressionTest
    {
        MessageBuffer buffer;
        [TestInitialize]
        public void Initialize()
        {
            buffer = new MessageBuffer();
        }
        [TestMethod]
        public void CanCompressBuffer()
        {
            buffer.Write("Hello World");
            buffer.Write(52);
            buffer.Write(52.0f);
            byte[] tmp = buffer.ToArray();
            byte[] data = new byte[tmp.Length - 1];

            Array.Copy(buffer.ToArray(), 1, data, 0, tmp.Length - 1);

            long lengthBefore = data.Length;
            byte[] compressed = MessageHelper.Compress(data);

            long lengthAfter = compressed.Length;
            Assert.AreNotEqual(lengthAfter, lengthBefore);
            
        }
        [TestMethod]
        public void CanDecompressBuffer()
        {
            buffer.Write((byte)1);
            buffer.Write("Hello World");
            buffer.Write(52);
            buffer.Write(52.0f);
            byte[] tmp = buffer.ToArray();
            byte[] data = new byte[tmp.Length - 1];

            Array.Copy(buffer.ToArray(), 1, data, 0, tmp.Length - 1);

            long lengthBefore = data.Length;
            byte[] compressed = MessageHelper.Compress(data);

            byte[] decompressed = MessageHelper.Decompress(compressed);

            buffer.Replace(decompressed);
            Assert.AreEqual<string>("Hello World", buffer.ReadString());
            Assert.AreEqual<int>(52, buffer.ReadInt32());
        }
        [TestMethod]
        public void CanCompressDecompressBigPacket()
        {
            // maximum packet size is 496
            int max_size = 496;
            int count = 0;
            while (buffer.Stream.Length < max_size)
            {
                buffer.Write("NewData" + count);
                buffer.Write(count++);
            }

            byte[] tmp = buffer.ToArray();
            byte[] data = new byte[tmp.Length];

            Array.Copy(buffer.ToArray(), 0, data, 0, tmp.Length);

            long lengthBefore = data.Length;
            byte[] compressed = MessageHelper.Compress(data);
            Assert.IsTrue(lengthBefore > compressed.Length);
            byte[] decompressed = MessageHelper.Decompress(compressed);
            buffer.Replace(decompressed);

            Assert.AreEqual<string>("NewData0", buffer.ReadString());
        }
    }
}
