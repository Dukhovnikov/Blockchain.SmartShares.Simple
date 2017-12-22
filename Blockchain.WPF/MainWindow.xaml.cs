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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartShares;

namespace Blockchain.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUploadAccount_Click(object sender, RoutedEventArgs e)
        {
            var coinPocketUser = DataManager.UploadUser();
            var personalArea = new WindowPersonalArea(coinPocketUser);

            this.Hide();
            personalArea.ShowDialog();
        }

        private void ButtonCreateNewAccoutn_Click(object sender, RoutedEventArgs e)
        {
            var createUserWindows = new WindowCreateUser();

            createUserWindows.ShowDialog();
        }

    }
}
