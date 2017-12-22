using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShares
{
    public static class ExtensionMethods
    {
        public static string ToJonHtml(this String str)
        {

            var sJsonHtml = str.Replace("\r\n", "<br/>");
            return sJsonHtml;
        }

    }
}
