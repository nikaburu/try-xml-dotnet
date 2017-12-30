using System.IO;
using static System.Console;

namespace Try.Xml.Console
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var dataFolder = @"\Data";
			if (args.Length > 0) dataFolder = args[0];

			var pathToXml = Path.Combine(dataFolder, "data.xml");

			var xmlTry = new XmlTry();

			WriteLine("Try Xdocument: ");
			var doc = xmlTry.TryXdocument(pathToXml);
			WriteLine();

			WriteLine("Try XmlScheme: ");
			var errors = xmlTry.TryXmlScheme(Path.Combine(dataFolder, "data.xsd"), doc);
			if (errors.Count > 0)
				errors.ForEach(WriteLine);
			else
				WriteLine("Verified successfuly.");

			WriteLine();

			WriteLine("Try XmlReader: ");
			xmlTry.TryXmlReader(pathToXml);
			WriteLine();

			WriteLine("Try XmlReaderSubTree: ");
			xmlTry.TryXmlReaderSubTree(pathToXml);
			WriteLine();

			WriteLine("Try XpathDocument: ");
			xmlTry.TryXpathDocument(pathToXml);
			WriteLine();

			WriteLine("Try XslTransformation: ");
			xmlTry.TryXslTransformation(pathToXml, Path.Combine(dataFolder, "data.xslt"));
			WriteLine();
		}
	}
}