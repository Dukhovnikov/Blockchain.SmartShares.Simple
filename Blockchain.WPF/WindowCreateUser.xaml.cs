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
    /// Логика взаимодействия для WindowCreateUser.xaml
    /// </summary>
    public partial class WindowCreateUser : Window
    {
        public WindowCreateUser()
        {
            InitializeComponent();
        }

        private void ButtonUploadAccount_Click(object sender, RoutedEventArgs e)
        {
            EccService.GenerateKey(out var privateKey, out var publicKey);

            var keyPair = new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };

            var newUser = new CoinPocket()
            {
                UserName = TextBoxUsername.Text,
                KeyPair = keyPair,
                ReceivePort = 6666
            };

            DataManager.SaveUser(newUser);

            this.Close();
        }

    }
}
