using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SmartShares;

namespace Blockchain.WebApplication
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextArea1.Value = DataManager.UploadBlockchain();
        }
    }
}