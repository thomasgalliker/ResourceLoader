using System.Collections.Generic;
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

        public IEnumerable<string> GetEmbeddedResourceNames(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            return resourceNames;
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

        public IEnumerable<Stream> GetEmbeddedResourceStreams(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames.Where(x => x.Contains(resourceFileName)).ToArray();
            foreach (var resourcePath in resourcePaths)
            {
                yield return assembly.GetManifestResourceStream(resourcePath);
            }
        }

        public byte[] GetEmbeddedResourceByteArray(Assembly assembly, string resourceFileName)
        {
            var stream = this.GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public IEnumerable<byte[]> GetEmbeddedResourceByteArrays(Assembly assembly, string resourceFileName)
        {
            var streams = this.GetEmbeddedResourceStreams(assembly, resourceFileName);

            foreach (var stream in streams)
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    yield return memoryStream.ToArray();
                }
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

        public IEnumerable<string> GetEmbeddedResourceStrings(Assembly assembly, string resourceFileName, Encoding encoding = null)
        {
            var streams = this.GetEmbeddedResourceStreams(assembly, resourceFileName);

            encoding = encoding ?? Encoding.UTF8;

            foreach (var stream in streams)
            {
                using (var streamReader = new StreamReader(stream, encoding))
                {
                    yield return streamReader.ReadToEnd();
                }
            }
        }
    }
}