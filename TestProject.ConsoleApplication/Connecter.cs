using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Blockchain.ConsoleApplication
{
    public class Connecter
    {
        public string remoteAddress; // хост для отправки данных
        public int remotePort; // порт для отправки данных
        public int localPort; // локальный порт для прослушивания входящих подключений

        public Connecter(int localPort, int remotePort, string remoteAddress)
        {
            this.localPort = localPort;
            this.remotePort = remotePort;
            this.remoteAddress = remoteAddress;
        }
        
        public void SendMessage(byte[] forwardBytes)
        {
            var sender = new UdpClient(); // создаем UdpClient для отправки сообщений
            try
            {
                while(true)
                {
                    //var message = Console.ReadLine(); // сообщение для отправки
                    //var data = Encoding.Unicode.GetBytes(message);
                    sender.Send(forwardBytes, forwardBytes.Length, remoteAddress, remotePort); // отправка
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public static void ReceiveMessage()
        {
            var localPort = 8888;
            
            var receiver = new UdpClient(localPort); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while(true)
                {
                    var data = receiver.Receive(ref remoteIp); // получаем данные
                    var message = Encoding.Unicode.GetString(data);
                    Console.WriteLine("Собеседник: {0}", message);
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
    }
}