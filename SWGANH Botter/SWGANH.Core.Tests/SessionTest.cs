using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWGANH.Core.Tests
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void CanDecompressMessage()
        {
        }
    }
    // this is just for testing the server aspect
    public class UdpServer
    {
        public UdpClient udp;
        public int port;

        UdpServer(int port)
        {
            this.port = port;
            udp = new UdpClient(this.port);
        }
        public async Task<UdpReceiveResult> ReceiveNormalMessage(string message)
        {
            return await udp.ReceiveAsync();
        }

        public void SendCompressedMessage(string message)
        {
            
        }

        //public static bool Decompress(byte[] message)
        //{
        //    bool result = false;

        //    Inflater inflater = new Inflater(false);
        //    // if it's negative we can't do anything so just return...
        //    if ((message.Length - RCV.SOE_OPCODE - 3) < 0)
        //        return result;
        //    try
        //    {

        //        //inflater.SetInput(message.HeapReference,
        //        //                    message.HeapOffset + RCV.SOE_SEQ,
        //        //                    (message.Length - RCV.SOE_OPCODE - 3));

        //        //// TODO Switch this
        //        //byte[] tmpbuffer = new byte[1024];
                
        //        //inflater.Inflate(tmpbuffer);
        //        //Array.Copy(tmpbuffer, 0, message.HeapReference, message.HeapOffset + RCV.SOE_SEQ, inflater.TotalOut);
        //        //message.Size = RCV.SOE_SEQ + (int)inflater.TotalOut;
        //        result = true;
        //    }
        //    catch (SharpZipBaseException ex)
        //    {
        //        Console.WriteLine("decompression error : {0}", ex.Message);
        //    }


        //    return result;
        //}
            
    }
}
