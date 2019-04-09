using System.Collections.Generic;
using System.Xml;

namespace BellTest.Codes
{
    /// <summary>
    /// Defines a set of bell signals.
    /// </summary>
    public class CodeList
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BellCode> Codes { get; set; } 

        public CodeList()
        {
            Codes = new List<BellCode>();
        }

        /// <summary>
        /// Load a CodeList from an XML document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static CodeList[] Load(XmlDocument document)
        {
            if (document == null || document.DocumentElement == null)
            {
                return null;
            }
            if (document.DocumentElement.Name != "codelists")
            {
                return null;
            }

            List<CodeList> lists = new List<CodeList>();
            foreach (XmlNode outerNode in document.DocumentElement.ChildNodes)
            {
                if (outerNode.Name != "codelist" || outerNode.Attributes == null)
                {
                    continue;
                }
                CodeList list = new CodeList { Name = outerNode.Attributes["name"].Value, Description = outerNode.Attributes["description"].Value };
                foreach (XmlNode node in outerNode.ChildNodes)
                {
                    if (node.Name == "code")
                    {
                        list.Codes.Add(ParseCode(node));
                    }
                }

                lists.Add(list);
            }

            return lists.ToArray();
        }

        private static BellCode ParseCode(XmlNode node)
        {
            if (node.Attributes == null)
            {
                return null;
            }
            BellCode code = new BellCode { Name = node.Attributes["name"].Value };
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name == "g")
                {
                    code.BellGroups.Add(ParseGroup(childNode));
                }
            }

            XmlAttribute releaseAttr = node.Attributes["release"];
            if (releaseAttr != null && releaseAttr.Value == "switch")
            {
                code.IsSwitchingRelease = true;
            }
            return code;
        }

        private static BellGroup ParseGroup(XmlNode node)
        {
            BellGroup group = new BellGroup();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name == "b")
                {
                    group.Bells.Add(BellStroke.Normal);
                }
                else if (childNode.Name == "h")
                {
                    group.Bells.Add(BellStroke.Hold);
                }
            }

            return group;
        }

        /// <summary>
        /// Load a CodeList from an XML file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static CodeList[] Load(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return Load(doc);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
