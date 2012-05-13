using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace SWGANH.Core
{
    public class MessageBuffer
    {
        public MemoryStream Stream { get; set; }
        BinaryWriter writer;
        BinaryReader reader;
        Encoding encoding = Encoding.Default;
        private byte[] p;
        public long WritePosition { get; set; }
        public long ReadPosition { get; set; }

        public MessageBuffer()
        {
            Stream = new MemoryStream();
            writer = new BinaryWriter(Stream);
            reader = new BinaryReader(Stream);
        }

        public MessageBuffer(byte[] data)
        {
            Stream = new MemoryStream(data);
            writer = new BinaryWriter(Stream);
            reader = new BinaryReader(Stream);
        }
        ~MessageBuffer()
        {
            Stream.Close();
        }
        public void Replace(byte[] bytes)
        {
            Stream = new MemoryStream(bytes);
            writer = new BinaryWriter(Stream);
            reader = new BinaryReader(Stream);
            ReadPosition = 0;
            WritePosition = 0;
        }
        public void ReplaceStream(Stream stream)
        {
            stream.CopyTo(Stream);
        }
        public byte[] ToArray()
        {
            return Stream.ToArray();
        }
        /// <summary>
        /// Writes a value to the current MemoryStream
        /// </summary>
        /// <typeparam name="T">Type to write</typeparam>
        /// <param name="item">Value to write</param>
        //public void Write<T>(T item)
        //{
        //    stream.Position = WritePosition;
        //    formatter.Serialize(stream, item);
        //    WritePosition = stream.Position;
        //}
        public void Write(byte v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(byte[] v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(char v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(char[] v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(float v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(Int16 v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(Int32 v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(Int64 v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void Write(string v)
        {
            Stream.Position = WritePosition;
            writer.Write(v);
            WritePosition = Stream.Position;
        }
        public void WriteNullTerminatedString(string str)
        {
            if (str.Length == 0)
                return;
            str += '\0';
            byte[] buffer = encoding.GetBytes(str);
                        
            Write(buffer);            
        }

        public byte ReadByte()
        {
            Stream.Position = ReadPosition;
            byte v = reader.ReadByte();
            ReadPosition = Stream.Position;
            return v;
        }
        public Int16 ReadInt16()
        {
            Stream.Position = ReadPosition;
            Int16 v = reader.ReadInt16();
            ReadPosition = Stream.Position;
            return v;
        }
        public Int32 ReadInt32()
        {
            Stream.Position = ReadPosition;
            Int32 v = reader.ReadInt32();
            ReadPosition = Stream.Position;
            return v;
        }
        public Int64 ReadInt64()
        {
            Stream.Position = ReadPosition;
            Int64 v = reader.ReadInt64();
            ReadPosition = Stream.Position;
            return v;
        }
        public string ReadString()
        {
            Stream.Position = ReadPosition;
            string v = reader.ReadString();
            ReadPosition = Stream.Position;
            return v;
        }
        public float ReadFloat()
        {
            Stream.Position = ReadPosition;
            decimal v = reader.ReadDecimal();
            ReadPosition = Stream.Position;
            return (float)v;
        }

        public string ReadNullTerminatedString()
        {
            Stream.Position = ReadPosition;
            char currentChar = 'x';
            char[] bytesRead = new char[Stream.Length - Stream.Position];
            int length = 0;
            while (currentChar != '\0')
            {
                currentChar = reader.ReadChar();
                bytesRead[length++] = currentChar;
            }
            // adjust size
            char[] chars = new char[length-1];
            Array.Copy(bytesRead, 0, chars, 0, length-1);
            string newStr = new string(chars);
            ReadPosition = Stream.Position;
            return newStr;
        }
    }
}
