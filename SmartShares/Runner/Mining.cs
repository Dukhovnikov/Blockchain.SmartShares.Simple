using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using MessagePack;

// ReSharper disable once CheckNamespace
namespace SmartShares
{
    public class Mining
    {
        private static Dictionary<string, Block> CurrentBlockchain { get; set; } 
            = DataManager.UploadBlockchainDictionary();

        
        public static bool RunMining()
        {
            
            var receiver = new UdpClient(8889); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            
            try
            {
                while(true)
                {
                    var data = receiver.Receive(ref remoteIp); // получаем данные
                    var transaction = MessagePackSerializer.Deserialize<Transaction>(data);

                    var newBlock = ComputeBlock(transaction);
                    //CurrentBlockchain.Add(CurrentBlockchain.Values.Last().Hash, newBlock);
                    
                    break;
                }

                var messageforsending = Encoding.UTF8.GetBytes("Your transaction was successful added by some miner");
                SendMessage(messageforsending, remoteIp.Port);
               // FileManager.UpdateBlockchain(MessagePackSerializer.Serialize(CurrentBlockchain));

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
            
            return true;
        }
        
        public static void SendMessage(byte[] data, int TransmitterPort)
        {
            var senderUdpClient = new UdpClient();

            try
            {

                senderUdpClient.Send(
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
                senderUdpClient.Close();
            }
        }

        public static Block ComputeBlock(Transaction transaction)
        {
            CurrentBlockchain = DataManager.UploadBlockchainDictionary();
            
            var check = true;
            var previousNonce = CurrentBlockchain.Values.Last().Nonce;
            var nonce = FindHash(previousNonce + 1);        
            
            while (check)
            {
                if (!CurrentBlockchain.ContainsKey(HexConvert.FromBytes(Hash.ComputeSha256FromString(nonce.ToString()))))
                {
                    check = false;
                }
                else
                {
                    nonce = FindHash(nonce + 1);
                }
            }

            var newBlock = new Block()
            {
                Id = CurrentBlockchain.Values.Last().Id + 1,
                Hash = Hash.ComputeSha256FromString(nonce.ToString()),
                PreviousHash = HexConvert.ToBytes(CurrentBlockchain.Keys.Last()),
                Nonce = nonce,
                Timestamp = DateTime.Now,
                Transaction = transaction
            };

            return newBlock;
        }

        /// <summary>
        /// Поиск хэша 
        /// </summary>
        private static int FindHash(int nonce)
        {
            for (int i = nonce; i < 100000000; i++)
            {
                var dataHash = Hash.ComputeSha256FromString(i.ToString());
                var dataHashStr = HexConvert.FromBytes(dataHash); 

                if (dataHashStr[dataHashStr.Length - 2].Equals('0'))
                {
                    return i;
                }
            }
            return -1;
        }
        
    }
}