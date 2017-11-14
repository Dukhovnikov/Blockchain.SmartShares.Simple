using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Blockchain.SmartShares;
using Blockchain.SmartShares.Network;


namespace Blockchain.ConsoleApplication
{
    internal static class Program
    {       
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите порт для прослушивания: ");
                var localPort = int.Parse(Console.ReadLine());
                
                Console.Write("Введите удаленный порт для подключения: ");
                var remotePort = int.Parse(Console.ReadLine());
                
                ConnectionManager.TransmitterPort = remotePort;
                ConnectionManager.ReceiverPort = localPort;
                
                var receiveThread = new Thread(new ThreadStart(ConnectionManager.ReceiveMessage));
                receiveThread.Start();
                
                Console.Write("Введите сообщение для отправки: ");
                var testMessage = Hash.ComputeSha256FromString(Console.ReadLine());
                
                ConnectionManager.SendMessage(testMessage);
            }          
                
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}