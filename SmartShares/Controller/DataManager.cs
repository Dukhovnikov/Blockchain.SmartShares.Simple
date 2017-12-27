using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


// ReSharper disable once CheckNamespace
namespace SmartShares
{
    public static class DataManager
    {
     
       /// <summary>
        /// Шаблон для бъявления пути к типам данных
        /// </summary>
        public static readonly Dictionary<string, string> FileDictionary = new Dictionary<string, string>
        {
            {"Blockchain", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blockchain\\Blockchain.txt"},
            {"Data", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blockchain\\Data"},
            {"Users", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Blockchain\\Users"}
        };
        
        /// <summary>
        /// Загрузка данных пользователя через диалог
        /// </summary>
        public static CoinPocket UploadUser()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                throw new Exception("Intorrect choose data file");

            var coinPocket =
                JsonConvert.DeserializeObject<CoinPocket>(File.ReadAllText(openFileDialog.FileName));

            return coinPocket;
        }

        /// <summary>
        /// Загрузка данных пользователя посредством ввода имени пользователя
        /// </summary>
        public static CoinPocket UploadUser(string nameUser)
        {
            var coinPocket =
                JsonConvert.DeserializeObject<CoinPocket>(
                    File.ReadAllText(FileDictionary["Users"] + $"\\{nameUser}.txt"));

            return coinPocket;
        }

        /// <summary>
        /// Сохранение данных нового пользователя
        /// </summary>
        public static void SaveUser(CoinPocket user)
        {
            var dataforsave = JsonConvert.SerializeObject(user, Formatting.Indented);

            File.WriteAllText(FileDictionary["Users"] + $"\\{user.UserName}.txt", dataforsave);
        }

        /// <summary>
        /// Добавляет в блокчейн новый блок
        /// </summary>
        public static void UpdateBlockchain(KeyValuePair<string, Block> data)
        {
            var jsonBlockhain = File.ReadAllText(FileDictionary["Blockchain"]);
            var blockchain = JsonConvert.DeserializeObject<Dictionary<string, Block>>(jsonBlockhain);
            blockchain.Add(data.Key, data.Value);

            File.WriteAllText(FileDictionary["Blockchain"], JsonConvert.SerializeObject(blockchain, Formatting.Indented));
        }

        /// <summary>
        /// Загрузка блокчейна из указанной директории
        /// </summary>
        public static string UploadBlockchain()
        {
            var jsonBlockchain = File.ReadAllText(FileDictionary["Blockchain"]);
            return jsonBlockchain;
        }

        /// <summary>
        /// Загрузка блокчейна из указанной директории
        /// </summary>
        public static Dictionary<string, Block> UploadBlockchainDictionary()
        {
            var jsonBlockchain = File.ReadAllText(FileDictionary["Blockchain"]);
            var blockchain = JsonConvert.DeserializeObject<Dictionary<string, Block>>(jsonBlockchain);
            return blockchain;
        }
    }
}