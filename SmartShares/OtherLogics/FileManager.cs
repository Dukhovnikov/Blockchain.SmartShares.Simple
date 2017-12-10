using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SmartShares
{
    public static class FileManager
    {
        public enum FileTypeofBlockchain
        {
            KeyPair,
            Block,
            Blockchain,
        }

        public static readonly string DataDirectoryPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            private static Dictionary<FileTypeofBlockchain, string> FileDictionary { get; } =
            new Dictionary<FileTypeofBlockchain, string>()
            {
                {FileTypeofBlockchain.KeyPair, "\\KeyPair.txt"},
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

        public static string CombainPath(FileTypeofBlockchain filetype)
        {
            return DataDirectoryPath + $"\\{FileDictionary[filetype]}";
        }

        public static Dictionary<byte[], Block> LoadBlockchain(string path)
        {
            var blockchainBytes = File.ReadAllBytes(path);

            return BlockchainUtil.DeserializeBlockchain(blockchainBytes);
        }
    }
}