using System;
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
	public class XmlTry
	{
		public XDocument TryXdocument(string path)
		{
			var document = XDocument.Load(path);
			WriteLine(document.Descendants().First().ToString());

			//document.Save(@"E:\questions.xml");
			return document;
		}

		public List<string> TryXmlScheme(string path, XDocument document)
		{
			//http://msdn.microsoft.com/ru-ru/library/ms256235.aspx
			var schemas = new XmlSchemaSet();
			schemas.Add(null, path);

			var errors = new List<string>();
			document.Validate(schemas, (o, e) => errors.Add(e.Message));

			return errors;
		}

		public void TryXmlReader(string path)
		{
			Func<XmlReader, string> readAttributes = reader =>
			{
				var attributes = new StringBuilder(reader.AttributeCount);
				if (reader.MoveToFirstAttribute())
					do
					{
						attributes.Append(string.Format("{0}=\"{1}\" ", reader.Name, reader.Value));
					} while (reader.MoveToNextAttribute());

				attributes.Replace(" ", "", attributes.Length - 1, 1);

				return attributes.ToString();
			};

			var settings = new XmlReaderSettings();
			settings.IgnoreComments = true;
			settings.DtdProcessing = DtdProcessing.Ignore;
			settings.ConformanceLevel = ConformanceLevel.Document;
			settings.IgnoreWhitespace = true;

			using (var reader = XmlReader.Create(path, settings))
			{
				while (reader.Read())
				{
					Write(new string(' ', reader.Depth * 2));

					switch (reader.NodeType)
					{
						case XmlNodeType.XmlDeclaration:
							WriteLine(reader.HasAttributes
								? string.Format("<?{0} {1}?>", reader.Name, readAttributes(reader))
								: string.Format("<?{0} ?>", reader.Name));
							break;
						case XmlNodeType.Element:
							WriteLine(reader.HasAttributes
								? string.Format("<{0} {1}>", reader.Name, readAttributes(reader))
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

		public void TryXmlReaderSubTree(string path)
		{
			using (var reader = XmlReader.Create(path))
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

		public void TryXpathDocument(string path)
		{
			var document = new XPathDocument(path);
			var navigator = document.CreateNavigator();
			//http://www.w3schools.com/xpath/xpath_syntax.asp
			var expression = XPathExpression.Compile("questions/question[@identity='0002']");

			foreach (XPathNavigator question in navigator.Select(expression))
				using (var subTreeReader = question.ReadSubtree())
				{
					var fakeDoc = XDocument.Load(subTreeReader);
					WriteLine(fakeDoc.ToString());
				}
		}

		public void TryXslTransformation(string path, string pathToStyleSheet)
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