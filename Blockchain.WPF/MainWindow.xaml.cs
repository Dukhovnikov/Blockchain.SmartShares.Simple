using System.Windows;
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
