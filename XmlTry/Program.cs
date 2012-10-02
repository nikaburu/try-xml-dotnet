using System;

namespace XmlTry
{
    internal class Program
    {
        //const string Path = @"E:\Ddisk\Else\CSh\Projects\Console\XmlTry\XmlTry.Web\App_Data\";
        static string _path = @"D:\WORK\Projects\MyProjects\Training projs\XmlTry\XmlTry.Web\App_Data\";

        static void Main(string[] args)
        {
            if (args.Length > 0) _path = args[0];

            XmlTry xmlTry = new XmlTry();
            
            Console.WriteLine("TryXdocument: ");
            var doc = xmlTry.TryXdocument(_path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXmlScheme: ");
            var errors = xmlTry.TryXmlScheme(_path + "data.xsd", doc);
            if (errors.Count > 0 )
            {
                errors.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Verified successfuly.");
            }
            Console.WriteLine();

            Console.WriteLine("TryXmlReader: ");
            xmlTry.TryXmlReader(_path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXmlReaderSubTree: ");
            xmlTry.TryXmlReaderSubTree(_path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXpathDocument: ");
            xmlTry.TryXpathDocument(_path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXslTransformation: ");
            xmlTry.TryXslTransformation(_path + "data.xml", _path + "data.xslt");
            Console.WriteLine();
        }
    }
}
