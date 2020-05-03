using System.Xml;

namespace Yahoo.DataModel
{
    internal class NSMgr : XmlNamespaceManager
    {
        private static string DefaultPrefix = "ns";

        public NSMgr(XmlDocument doc)
            : base(doc.NameTable)
        {
            AddNamespace(DefaultPrefix, "http://fantasysports.yahooapis.com/fantasy/v2/base.rng");
        }

        public static string GetNodeName(string name)
        {
            return DefaultPrefix + ":" + name;
        }

        public string GetXPath(params string[] hierarchy)
        {
            string xpath = GetNodeName(hierarchy[0]);
            for (int i = 1; i < hierarchy.Length; i++)
            {
                xpath += "/" + GetNodeName(hierarchy[i]);
            }

            return xpath;
        }

        public string GetValue(XmlNode root, params string[] hierarchy)
        {
            string xpath = GetXPath(hierarchy);
            XmlNode child = root.SelectSingleNode(xpath, this);
            if (child == null)
                return string.Empty;
            return child.InnerText;
        }
    }
}
