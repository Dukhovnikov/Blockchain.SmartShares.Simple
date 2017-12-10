using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShares
{
    public class ConnectionManager
    {
        public static int ReceiverPort { get; set; }
        
        public static int TransmitterPort { get; set; }

        public static void SendMessage(byte[] data)
        {
            var senderUDPClient = new UdpClient();

            try
            {

                    senderUDPClient.Send(
                        data, 
                        data.Length, 
                        "127.0.0.1", 
                        TransmitterPort);
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
            var receiver = new UdpClient(ReceiverPort); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            
            try
            {
                while(true)
                {
                    var data = receiver.Receive(ref remoteIp); // получаем данные
                    Console.WriteLine($"Собеседник: {HexConvert.FromBytes(data)}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
        
        public static async Task<byte[]> ReceiveMessageAsync(UdpClient client)
        {
            UdpReceiveResult result;
            
            try
            {
                result = await client.ReceiveAsync();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result.Buffer;
        }

        /*public static async Task<byte[]> ReceiveMessageAsync(UdpClient client, )
        {            
            var receiver = new UdpClient(ReceiverPort);
            var receiveData = new byte[0];
            IPEndPoint remotePoint = null;

            try
            {
                while (true)
                {
                    await receiver.ReceiveAsync();
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
            
            
        }*/

    }
}