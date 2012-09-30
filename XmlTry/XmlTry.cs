using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace XmlTry
{
    public class XmlTry
    {
        public XDocument TryXdocument(string path)
        {
            XDocument document = XDocument.Load(path);
            Console.WriteLine(document.ToString());

            //document.Save(@"E:\questions.xml");
            return document;
        }

        public List<string> TryXmlScheme(string path, XDocument document)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, path);

            List<string> errors = new List<string>();
            document.Validate(schemas, (o, e) => errors.Add(e.Message));

            return errors;
        }

        public void TryXmlReader(string path)
        {
            Func<XmlReader, string> readAttributes = reader =>
            {
                StringBuilder attributes = new StringBuilder(reader.AttributeCount);
                if (reader.MoveToFirstAttribute())
                    do
                    {
                        attributes.Append(string.Format("{0}=\"{1}\" ", reader.Name, reader.Value));
                    }
                    while (reader.MoveToNextAttribute());

                attributes.Replace(" ", "", attributes.Length - 1, 1);

                return attributes.ToString();
            };

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.IgnoreWhitespace = true;
            
            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                while (reader.Read())
                {
                    Console.Write(new string(' ', reader.Depth * 2));

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.XmlDeclaration:
                            Console.WriteLine(reader.HasAttributes
                                                  ? string.Format("<?{0} {1}?>", reader.Name, readAttributes(reader))
                                                  : string.Format("<?{0} ?>", reader.Name));
                            break;
                        case XmlNodeType.Element:
                            Console.WriteLine(reader.HasAttributes
                                                  ? string.Format("<{0} {1}>", reader.Name, readAttributes(reader))
                                                  : string.Format("<{0}>", reader.Name));
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(reader.Value.Trim());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine(string.Format("</{0}>", reader.Name));
                            break;
                        default:
                            Console.WriteLine(reader.NodeType + ": " + reader.Name);
                            break;
                    }
                }
            }
        }

        public void TryXmlReaderSubTree(string path)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read() && reader.Name != "closedQuestion") { }

                using (XmlReader subTreeReader = reader.ReadSubtree())
                {
                    XDocument document = XDocument.Load(subTreeReader);
                    Console.WriteLine(document.ToString());
                }
            }
        }

        public void TryXpathDocument(string path)
        {
            XPathDocument document = new XPathDocument(path);
            XPathNavigator navigator = document.CreateNavigator();
            XPathExpression expression = XPathExpression.Compile("questions/openedQuestion");

            foreach (XPathNavigator question in navigator.Select(expression))
            {
                using (XmlReader subTreeReader = question.ReadSubtree())
                {
                    XDocument fakeDoc = XDocument.Load(subTreeReader);
                    Console.WriteLine(fakeDoc.ToString());
                }
            }
        }
    }
}
