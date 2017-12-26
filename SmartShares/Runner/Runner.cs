using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagePack;
using Newtonsoft.Json;

namespace SmartShares
{
    public class Runner
    {
        private static Dictionary<byte[], Block> Blockchain { get; set; }
        
        public void Run()
        {
            Blockchain = FileManager.LoadBlockchain();
            
            var pocket = new CoinPocket();

            switch (Menu())
            {
                case "1":
                    var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                        throw new Exception("Intorrect choose data file");
                    
                    var text = File.ReadAllText(openFileDialog.FileName);
                    pocket = JsonConvert.DeserializeObject<CoinPocket>(text);                                      
                    break;
                    
                case "2":
                    pocket = ConsoleWorker.CreateNewUser();
                    
                    var openFileDialog2 = new SaveFileDialog();
                    if (openFileDialog2.ShowDialog() == DialogResult.Cancel) 
                        throw new Exception("Intorrect choose data file");
                    
                    File.WriteAllText(openFileDialog2.FileName, 
                        JsonConvert.SerializeObject(pocket, Formatting.Indented));
                    break;

                default:

                    break;
            }
            
            ConsoleWorker.WriteDataCoinPocket(pocket);

            var check = true;
            while (check)
            {
                Console.WriteLine();
                
                ConnectionManager.ReceiverPort = pocket.ReceivePort;
                var receivemessage = ConnectionManager.ReceiveMessageAsync();
                
                if (receivemessage.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(receivemessage.Result));
                }
                
                switch (SubMenu())
                {
                    case "1":
                        Console.WriteLine(JsonConvert.SerializeObject(pocket, Formatting.Indented));
                        break;
                    case "2":
                        var blockchain = FileManager.LoadBlockchain();
                        Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));
                        break;

                    case "3":
                        ConsoleWorker.WriteDataCoinPocket(pocket);
                        break;

                    case "4":
                        Console.WriteLine();                        

                        var openFileDialog = new OpenFileDialog();
                        if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                            throw new Exception("Intorrect choose data file");

                        var recipientCoinPocket =
                            JsonConvert.DeserializeObject<CoinPocket>(File.ReadAllText(openFileDialog.FileName));


                        var publicKeyRecipient = recipientCoinPocket.KeyPair.PublicKey; 
                        
                        Console.WriteLine();
                        Console.WriteLine("Please, enter amount ACT would you want to send:");

                        var amountACTforsend = int.Parse(Console.ReadLine());

                        var inEntry = new InEntry()
                        {
                            //PreviuosOut = Blockchain.Values.Last().Transaction.OutEntries.Last().RecipientHash,
                            PublicKey = pocket.KeyPair.PublicKey,
                            Amount = (int) amountACTforsend
                        };

                        var outEntry = new OutEntry()
                        {
                            RecipientHash = publicKeyRecipient,
                            Value = amountACTforsend
                        };
                        
                        var outEntryOwner = new OutEntry()
                        {
                            RecipientHash = pocket.KeyPair.PublicKey,
                            Value = CoinPocketManager.ParseFromBlockainFix(Blockchain, pocket.KeyPair.PublicKey) - amountACTforsend
                        };

                        var transaction = new Transaction()
                        {
                            Id = 0,
                            InEntries = new List<InEntry>() {inEntry},
                            OutEntries = new List<OutEntry>() {outEntry, outEntryOwner},
                            Signature = EccService.Sign(
                                Hash.ComputeSha256FromString(JsonConvert.SerializeObject(outEntry)),
                                pocket.KeyPair.PrivateKey,
                                pocket.KeyPair.PublicKey),
                            Timestamp = DateTime.Now
                        };


                        var transactionByte = MessagePackSerializer.Serialize(transaction);
                        
                        var senderUdpClient = new UdpClient();
                        
                        try
                        {

                            senderUdpClient.Send(
                                transactionByte, 
                                transactionByte.Length, 
                                "127.0.0.1", 
                                8889);
                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        finally
                        {
                            senderUdpClient.Close();
                        }
                        
                        break;
                        
                    case "5":
                        Mining.RunMining();
                        
                        break;

                    default:
                        check = false;
                        break;
                }
                
                Blockchain = FileManager.LoadBlockchain();
            }

        }
        
        private string Menu()
        {
            Console.WriteLine("1. Upload your Account");
            Console.WriteLine("2. Create new Account");
            return Console.ReadLine();
        }
        
        private string SubMenu()
        {
            Console.WriteLine("1. View your pr/rup keys");
            Console.WriteLine("2. View blockchain");
            Console.WriteLine("3. View information about your wallet");
            Console.WriteLine("4. Making transaction");
            Console.WriteLine("5. Run maining");
            return Console.ReadLine();
        }

    }
}