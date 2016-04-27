using System.IO;
using System.Linq;
using System.Reflection.Exceptions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
    /// <summary>
    ///     Utility that can be used to find and load embedded resources into memory.
    /// </summary>
    public class ResourceLoader : IResourceLoader
    {
        static readonly Lazy<IResourceLoader> Implementation = new Lazy<IResourceLoader>(CreateResourceLoader, LazyThreadSafetyMode.PublicationOnly);

        public static IResourceLoader Current
        {
            get
            {
                return Implementation.Value;
            }
        }

        static IResourceLoader CreateResourceLoader()
        {
            return new ResourceLoader();
        }

        public Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames.Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase)).ToArray();

            if (!resourcePaths.Any())
            {
                throw new ResourceNotFoundException(resourceFileName);
            }

            if (resourcePaths.Length > 1)
            {
                throw new MultipleResourcesFoundException(resourceFileName, resourcePaths);
            }

            return assembly.GetManifestResourceStream(resourcePaths.Single());
        }

        public byte[] GetEmbeddedResourceBytes(Assembly assembly, string resourceFileName)
        {
            var stream = this.GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public string GetEmbeddedResourceString(Assembly assembly, string resourceFileName, Encoding encoding = null)
        {
            var stream = this.GetEmbeddedResourceStream(assembly, resourceFileName);

            encoding = encoding ?? Encoding.UTF8;

            using (var streamReader = new StreamReader(stream, encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}