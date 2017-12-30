using System;
using System.IO;
using static System.Console;

namespace Try.Xml.Console
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");
			if (args.Length > 0) dataFolder = args[0];

			var pathToXml = Path.Combine(dataFolder, "data.xml");

			var xmlSamples = new XmlSamples();

			WriteLine("Xdocument: ");
			var doc = xmlSamples.Xdocument(pathToXml);
			WriteLine();

			WriteLine("XmlScheme: ");
			var errors = xmlSamples.XmlScheme(Path.Combine(dataFolder, "data.xsd"), doc);
			if (errors.Count > 0)
				errors.ForEach(WriteLine);
			else
				WriteLine("Verified successfully.");

			WriteLine();

			WriteLine("XmlReader: ");
			xmlSamples.XmlReader(pathToXml);
			WriteLine();

			WriteLine("XmlReaderSubTree: ");
			xmlSamples.XmlReaderSubTree(pathToXml);
			WriteLine();

			WriteLine("XpathDocument: ");
			xmlSamples.XpathDocument(pathToXml);
			WriteLine();

			WriteLine("XslTransformation: ");
			xmlSamples.XslTransformation(pathToXml, Path.Combine(dataFolder, "data.xslt"));
			WriteLine();
		}
	}
}