﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessagePack;
using Newtonsoft.Json;
using SmartShares;
using Block = SmartShares.Block;
using Exception = System.Exception;

namespace Blockchain.WPF
{
    /// <summary>
    /// Логика взаимодействия для WindowPersonalArea.xaml
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public partial class WindowPersonalArea : Window
    {
        private readonly CoinPocket _userCoinPocket;
        private int Amount { get; set; }

        public WindowPersonalArea()
        {
            InitializeComponent();
        }

        public WindowPersonalArea(CoinPocket user)
        {
            InitializeComponent();
            
            _userCoinPocket = user;
            GroupBoxInformationAboutUser.Header = user.UserName;
            TextBlockId.Text = HexConvert.FromBytes(user.KeyPair.PublicKey);

            var blockchain = DataManager.UploadBlockchainDictionary();
            
/*            var amount = int.Parse(Executor
                .GetCash(DataManager.UploadBlockchainDictionary(), user.KeyPair.PublicKey).ToString());*/
            
            var amount = int.Parse(Executor
                .GetCashRec(blockchain, user.KeyPair.PublicKey, blockchain.Last().Key).ToString());
            
            Amount = amount;
            
            if (amount > -1)
            {
                TextBlockAct.Text = amount + " @";

            }
            else
            {
                TextBlockAct.Text = "Fail parse blockchain.";
            }

            TextBoxRecipientId.Text = "";
            TextBoxAmountAct.Text = "";
        }

        private void ButtonToPay_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(TextBoxAmountAct.Text) > Amount)
            {
                MessageBox.Show("Error!", "You haven't avaliable ACT for makking transaction.", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            
            var recipientUserHash = HexConvert.ToBytes(TextBoxRecipientId.Text);

            var inEntry = new InEntry()
            {
                PublicKey = _userCoinPocket.KeyPair.PublicKey,
                Amount = Amount
            };

            var outEntry = new List<OutEntry>(15)
            {
                new OutEntry()
                {
                    RecipientHash = recipientUserHash,
                    Value = int.Parse(TextBoxAmountAct.Text)
                },
                new OutEntry()
                {
                    RecipientHash = _userCoinPocket.KeyPair.PublicKey,
                    Value = Amount - int.Parse(TextBoxAmountAct.Text)
                }
            };

            var transaction = new Transaction()
            {
                Id = DataManager.UploadBlockchainDictionary().Last().Value.Transaction.Id + 1,
                InEntries = new List<InEntry>() {inEntry},
                OutEntries = outEntry,
                Signature = EccService.Sign(_userCoinPocket.KeyPair.PublicKey, _userCoinPocket.KeyPair.PrivateKey,
                    _userCoinPocket.KeyPair.PublicKey),
                Timestamp = DateTime.Now
            };

            var message = BlockchainUtil.SerializeTransaction(transaction);

            var senderUdpClient = new UdpClient(_userCoinPocket.ReceivePort);

            try
            {

                senderUdpClient.Send(
                    message,
                    message.Length,
                    "127.0.0.1",
                    9999);
            }

            catch
            {
                throw new Exception();
            }

            finally
            {
                senderUdpClient.Close();
            }

            var receiver = new UdpClient(_userCoinPocket.ReceivePort);
            IPEndPoint remoteIp = null;
            var data = receiver.Receive(ref remoteIp);
            var chain = MessagePackSerializer.Deserialize<KeyValuePair<string, Block>>(data);
            var json = JsonConvert.SerializeObject(chain, Formatting.Indented);
            GroupBoxStatus.Header = "You trancsaction has been added";

            var blockchain = DataManager.UploadBlockchainDictionary();
            TextBlockAct.Text = Executor
                .GetCashRec(blockchain, _userCoinPocket.KeyPair.PublicKey, blockchain.Last().Key).ToString();
            TextBlockStatus.Text = json;
            receiver.Close();
        }

        private void ButtonUploadRecipient_Click(object sender, RoutedEventArgs e)
        {
            var user = DataManager.UploadUser();

            TextBoxRecipientId.Text = HexConvert.FromBytes(user.KeyPair.PublicKey);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
