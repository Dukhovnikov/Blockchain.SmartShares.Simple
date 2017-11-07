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
        
        public Dictionary<FileTypeofBlockchain, string> FileDictionary = 
            new Dictionary<FileTypeofBlockchain, string>()
            {
                {FileTypeofBlockchain.KeyPairs, ""}
            }
        
        private static string SaveFilePath { get; set; }

        public static string ChooseFolderToSaveData()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
        
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            throw new Exception("Invalid choosing path");
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