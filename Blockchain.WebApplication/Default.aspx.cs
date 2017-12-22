using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        public void LinkButtonUploadMiningUser_Click(object sender, EventArgs e)
        {
            //var selectedUser = DropDownList1.SelectedValue;
            //var user = DataManager.UploadUser(selectedUser);
            //TextArea1.Value = JsonConvert.SerializeObject(user, Formatting.Indented);
            
            var receiver = new UdpClient(8889);
            IPEndPoint remoteIp = null;

            try
            {
                while (true)
                {
                    var data = receiver.Receive(ref remoteIp);
                    var json = Encoding.UTF8.GetString(data);
                    Session["last chain"] = JsonConvert.DeserializeObject<KeyValuePair<string, Block>>(json);

                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                var jsonByte = BlockchainUtil.SerializeJsonByteChain((KeyValuePair<string, Block>) Session["last chain"]);
                var jsonString = Encoding.UTF8.GetString(jsonByte);
                Label1.Text = jsonString.ToJonHtml();
                receiver.Close();
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    var receiver = new UdpClient(8889);
        //    IPEndPoint remoteIp = null;

        //    try
        //    {
        //        while (true)
        //        {
        //            var data = receiver.Receive(ref remoteIp);
        //            TextArea1.Value += data.ToString();
        //            break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        receiver.Close();
        //    }
        //}
    }
}