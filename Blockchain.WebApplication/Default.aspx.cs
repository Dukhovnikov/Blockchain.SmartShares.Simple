using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using SmartShares;
using  Newtonsoft.Json;
using MessagePack;
using Microsoft.Ajax.Utilities;
using Block = SmartShares.Block;

namespace Blockchain.WebApplication
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var files = Directory.GetFiles(DataManager.FileDictionary["Users"], "*.txt")
                    .Select(Path.GetFileNameWithoutExtension);

                DropDownList1.DataSource = files;
                DropDownList1.DataBind();

                Session["last chain"] = new KeyValuePair<string, Block>();
            }
        }

        protected void LinkButtonUploadMiningUser_Click(object sender, EventArgs e)
        {
            var selectedUser = DropDownList1.SelectedValue;
            var user = DataManager.UploadUser(selectedUser);
            //TextArea1.Value = JsonConvert.SerializeObject(user, Formatting.Indented);
            
            var receiver = new UdpClient(9999);
            IPEndPoint remoteIp = null;

            try
            {
                var data = receiver.Receive(ref remoteIp);
                var transaction = MessagePackSerializer.Deserialize<Transaction>(data);

                var additionalOutEntry = new OutEntry()
                {
                    RecipientHash = user.KeyPair.PublicKey,
                    Value = 10
                };

                transaction.OutEntries.Add(additionalOutEntry);
                
                var block = Mining.ComputeBlock(transaction);

                var chain = new KeyValuePair<string, Block>(
                    HexConvert.FromBytes(DataManager.UploadBlockchainDictionary().Last().Value.Hash),
                    block);

                DataManager.UpdateBlockchain(chain);
                
                Session["last chain"] = chain;
                
                Thread.Sleep(500);

                var senderUdpClient = new UdpClient();
                var message = MessagePackSerializer.Serialize(chain);
                senderUdpClient.Send(
                    message,
                    message.Length,
                    "127.0.0.1",
                    remoteIp.Port);
                senderUdpClient.Close();
            }

            finally
            {
                var json = JsonConvert.SerializeObject(
                    (KeyValuePair<string, Block>) Session["last chain"], Formatting.Indented);               
                receiver.Close();               
                
                TextArea1.Value = json;
                //Label1.Text = json;
                //Label1.Text = json.ToJonHtml();
            }
        }
    }
}