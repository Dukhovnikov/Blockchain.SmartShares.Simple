using System;
using System.Collections.Generic;
using System.Linq;
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
using SmartShares;
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

            var amount = int.Parse(Executor
                .ParseFromBlockain(DataManager.UploadBlockchainDictionary(), user.KeyPair.PublicKey).ToString());
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
            var recipientUserHash = HexConvert.ToBytes(TextBoxRecipientId.Text);

            var inEntry = new InEntry()
            {
                PublicKey = _userCoinPocket.KeyPair.PublicKey,
                PreviuosOut = _userCoinPocket.KeyPair.PublicKey
            };

            var outEntry = new List<OutEntry>()
            {
                new OutEntry()
                {
                    RecipientHash = recipientUserHash,
                    Value = ulong.Parse(TextBoxAmountAct.Text)
                },
                new OutEntry()
                {
                    RecipientHash = _userCoinPocket.KeyPair.PublicKey,
                    Value = (ulong) Amount - ulong.Parse(TextBoxAmountAct.Text)
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

            var senderUdpClient = new UdpClient();

            try
            {

                senderUdpClient.Send(
                    message,
                    message.Length,
                    "127.0.0.1",
                    8889);
            }

            catch
            {
                throw new Exception();
            }

            finally
            {
                senderUdpClient.Close();
            }
        }
    }
}
