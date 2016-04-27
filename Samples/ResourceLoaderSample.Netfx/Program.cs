using System;
using System.Reflection;

namespace ResourceLoaderSample.Netfx
{
    class Program
    {
        static void Main(string[] args)
        {
            var resourceAssembly = Assembly.Load("ResourceLoaderSample");
            string xmlFilePath = "XMLFile1.xml";

            string xmlContent = ResourceLoader.Current.GetEmbeddedResourceString(resourceAssembly, xmlFilePath);

            Console.WriteLine(xmlContent);
            Console.ReadLine();
        }
    }
}