using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWGANH.Core;

namespace SWGANH.Core.Tests
{
    [TestClass]
    public class MessageBufferTest
    {
        MessageBuffer buffer;
        [TestInitialize()]
        public void Initialize()
        {
            buffer = new MessageBuffer();
        }
        [TestMethod]
        public void CanWritePrimatives()
        {
            
            buffer.Write(25);
            buffer.Write("hi");
        }
        [TestMethod]
        public void AddingValuesIncrementsPosition()
        {
            Assert.AreEqual<long>(0, buffer.WritePosition);
            buffer.Write("kyle is cool");
            buffer.Write(42);
            Assert.AreNotEqual<long>(0, buffer.WritePosition);
        }
        [TestMethod]
        public void CanExtractValues()
        {
            buffer.Write("kyle is cool");
            buffer.Write(42);

            string kyle = buffer.ReadString();
            int universe = buffer.ReadInt32();

            Assert.AreEqual<string>("kyle is cool", kyle);
            Assert.AreEqual<int>(42, universe);
        }
        [TestMethod]
        public void CanWriteReadPascalString()
        {
            buffer.Write("This is a regular string");
            buffer.WriteNullTerminatedString("This is a pascal string");
            buffer.Write("this is another string");

            Assert.AreEqual<string>("This is a regular string", buffer.ReadString());
            Assert.AreEqual<string>("This is a pascal string", buffer.ReadNullTerminatedString());
            Assert.AreEqual<string>("this is another string", buffer.ReadString());
        }
        [TestMethod]
        public void CanReplaceBuffer()
        {
            buffer.Write("This is a regular string");
            buffer.WriteNullTerminatedString("This is a pascal string");
            buffer.Write("this is another string");

            MessageBuffer newBuffer = new MessageBuffer();
            newBuffer.Write("new and exciting");
            newBuffer.Write(25);

            buffer.Replace(newBuffer.ToArray());

            Assert.AreEqual<string>("new and exciting", buffer.ReadString());
            Assert.AreEqual<int>(25, buffer.ReadInt32());
        }
        [TestMethod]
        public void CanCreateBufferFromBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write("Testing");
                    writer.Write(12345678910);
                }

                buffer = new MessageBuffer(stream.ToArray());

                Assert.AreEqual<string>("Testing", buffer.ReadString());
                Assert.AreEqual<long>(12345678910, buffer.ReadInt64());
            }

            
        }
    }
}
