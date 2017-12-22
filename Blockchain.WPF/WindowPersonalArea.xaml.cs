using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Blockchain.WPF
{
    /// <summary>
    /// Логика взаимодействия для WindowPersonalArea.xaml
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public partial class WindowPersonalArea : Window
    {
        private readonly CoinPocket _userCoinPocket;

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
            TextBlockAct.Text = "12 @";

            TextBoxRecipientId.Text = "";
            TextBoxAmountAct.Text = "";
        }

        private void ButtonToPay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
