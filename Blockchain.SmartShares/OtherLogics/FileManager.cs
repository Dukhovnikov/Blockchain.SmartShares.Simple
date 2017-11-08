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

        public static readonly string DataDirectoryPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            private static Dictionary<FileTypeofBlockchain, string> FileDictionary { get; } =
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

        public static string CombainPath(FileTypeofBlockchain filetype)
        {
            return DataDirectoryPath + $"\\{FileDictionary[filetype]}";
        }                    
    }
}