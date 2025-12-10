using System.Reflection;

namespace ResourceLoader.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                $"ResourceLoader Sample App{Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            var assembly = typeof(Program).Assembly;
            var resourceName = "XMLFile1.xml";

            var content = IResourceLoader.Current.GetEmbeddedResourceString(assembly, resourceName);

            Console.WriteLine($"IResourceLoader.Current.GetEmbeddedResourceString({assembly.GetName().Name}, {resourceName}) returned:");
            Console.WriteLine(content);
            Console.ReadLine();
        }
    }
}
