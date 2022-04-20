using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace check_check.Database
{
    public class XmlManager
    {
        private static string configFile = @"../../Config.xml";
        public static string ConfigFile { get; set; }


        public static string GetValue(params string[] args)
        {
            string result = string.Empty;

            try
            {
                XDocument xDoc = XDocument.Load(configFile);
                result = GetNodeValue(xDoc.FirstNode as XElement, 0, args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private static string GetNodeValue(XElement node, int idx, params string[] args)
        {
            string result = args.Length > idx + 1 ? GetNodeValue(node.Element(args[idx]), ++idx, args) : node.Element(args[idx]).Value.ToString();
            return result;
        }
    }
}
