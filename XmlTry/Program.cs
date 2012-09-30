using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XmlTry
{
    class Program
    {
        const string Path = @"E:\Ddisk\Else\CSh\Projects\Console\XmlTry\XmlTry.Web\App_Data\";

        static void Main()
        {
            XmlTry xmlTry = new XmlTry();
            
            Console.WriteLine("TryXdocument: ");
            var doc = xmlTry.TryXdocument(Path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXmlScheme: ");
            var errors = xmlTry.TryXmlScheme(Path + "data.xsd", doc);
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
            xmlTry.TryXmlReader(Path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXmlReaderSubTree: ");
            xmlTry.TryXmlReaderSubTree(Path + "data.xml");
            Console.WriteLine();

            Console.WriteLine("TryXpathDocument: ");
            xmlTry.TryXpathDocument(Path + "data.xml");
            Console.WriteLine();
        }
    }
}
