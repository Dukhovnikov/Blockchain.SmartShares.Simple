using System;
using System.Net;
using System.Net.Sockets;

namespace Blockchain.SmartShares.Network
{
    public class ConnectionManager
    {
        private static int ReceiverPort { get; set; }
        
        private static int TransmitterPort { get; set; }

        public static void SendMessage(byte[] data)
        {
            var senderUDPClient = new UdpClient();

            try
            {
                while (true)
                {
                    senderUDPClient.Send(
                        data, 
                        data.Length, 
                        "172.0.0.1", 
                        TransmitterPort);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                senderUDPClient.Close();
            }
        }

        public static void ReceiveMessage()
        {
            var receiver = new UdpClient(ReceiverPort);
            IPEndPoint remotePoint = null;

            try
            {
                while (true)
                {
                    var data = receiver.Receive(ref remotePoint);
                    
                }
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            finally
            {
                receiver.Close();
            }

        }

    }
}