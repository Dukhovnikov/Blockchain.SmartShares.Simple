using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Blockchain.SmartShares;

namespace Blockchain.ConsoleApplication
{
    public static class FileManager
    {
        public enum FileTypeofBlockchain
        {
            KeyPairs,
            Block,
            Blockchain,
        }

        private static Dictionary<FileTypeofBlockchain, string> FileDictionary { get; }=
            new Dictionary<FileTypeofBlockchain, string>()
            {
                {FileTypeofBlockchain.KeyPairs, "\\KeyPairs.txt"},
                {FileTypeofBlockchain.Block, "\\Block.txt"},
                {FileTypeofBlockchain.Blockchain, "Blockchain.txt"}
            };
        
        public static string ChooseFolderToSaveData()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
        
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            throw new Exception("Invalid choosing path");
        }

        public static string CombainPath(string folderPath, FileTypeofBlockchain filetype)
        {
            return folderPath + $"\\{FileDictionary[filetype]}";
        }
        
        static void Test()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Text files(*.txt)|*.txt",
                Title = "Choose file of kay pair"
            };

            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new InvalidDataException("Collapsed keypair.");

            var path = openFileDialog.FileName;
            var keys = KeyPair.LoadFrom(path);

            var pocket = new CoinPocket()
            {
                KeyPair = keys,
                Amount = 1
            };
            
            ConsoleWorker.WriteDataCoinPocket(pocket);

            Console.ReadKey();
        }
        
        
    }
}