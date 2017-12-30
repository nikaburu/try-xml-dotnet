using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using static System.Console;

namespace Try.Xml.Console
{
	public class XmlSamples
	{
		public XDocument Xdocument(string path)
		{
			var document = XDocument.Load(path);
			WriteLine(document.Descendants().First().ToString());

			//document.Save(@"...");
			return document;
		}

		public List<string> XmlScheme(string path, XDocument document)
		{
			//http://msdn.microsoft.com/en-gb/library/ms256235.aspx
			var schemas = new XmlSchemaSet();
			schemas.Add(null, path);

			var errors = new List<string>();
			document.Validate(schemas, (o, e) => errors.Add(e.Message));

			return errors;
		}

		public void XmlReader(string path)
		{
			string ReadAttributes(XmlReader reader)
			{
				var attributes = new StringBuilder(reader.AttributeCount);
				if (reader.MoveToFirstAttribute())
					do
					{
						attributes.Append(string.Format("{0}=\"{1}\" ", reader.Name, reader.Value));
					} while (reader.MoveToNextAttribute());

				attributes.Replace(" ", "", attributes.Length - 1, 1);

				return attributes.ToString();
			}

			var settings = new XmlReaderSettings
			{
				IgnoreComments = true,
				DtdProcessing = DtdProcessing.Ignore,
				ConformanceLevel = ConformanceLevel.Document,
				IgnoreWhitespace = true
			};

			using (var reader = System.Xml.XmlReader.Create(path, settings))
			{
				while (reader.Read())
				{
					Write(new string(' ', reader.Depth * 2));

					switch (reader.NodeType)
					{
						case XmlNodeType.XmlDeclaration:
							WriteLine(reader.HasAttributes
								? string.Format("<?{0} {1}?>", reader.Name, ReadAttributes(reader))
								: string.Format("<?{0} ?>", reader.Name));
							break;
						case XmlNodeType.Element:
							WriteLine(reader.HasAttributes
								? string.Format("<{0} {1}>", reader.Name, ReadAttributes(reader))
								: string.Format("<{0}>", reader.Name));
							break;
						case XmlNodeType.Text:
							WriteLine(reader.Value.Trim());
							break;
						case XmlNodeType.EndElement:
							WriteLine(string.Format("</{0}>", reader.Name));
							break;
						default:
							WriteLine(reader.NodeType + ": " + reader.Name);
							break;
					}
				}
			}
		}

		public void XmlReaderSubTree(string path)
		{
			using (var reader = System.Xml.XmlReader.Create(path))
			{
				while (reader.Read() && reader.Name != "question")
				{
				}

				using (var subTreeReader = reader.ReadSubtree())
				{
					var document = XDocument.Load(subTreeReader);
					WriteLine(document.ToString());
				}
			}
		}

		public void XpathDocument(string path)
		{
			var document = new XPathDocument(path);
			var navigator = document.CreateNavigator();
			var expression = XPathExpression.Compile("questions/question[@identity='0002']");

			foreach (XPathNavigator question in navigator.Select(expression))
				using (var subTreeReader = question.ReadSubtree())
				{
					var fakeDoc = XDocument.Load(subTreeReader);
					WriteLine(fakeDoc.ToString());
				}
		}

		public void XslTransformation(string path, string pathToStyleSheet)
		{
			var transform = new XslCompiledTransform();
			transform.Load(pathToStyleSheet);

			using (var sw = new StringWriter())
			{
				transform.Transform(path, new XmlTextWriter(sw));
				WriteLine(sw.ToString());
			}
		}
	}
}