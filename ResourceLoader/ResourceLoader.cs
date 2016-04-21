using System.IO;
using System.Linq;
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
                // TODO: Use custom exception here
                throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));
            }

            if (resourcePaths.Length > 1)
            {
                // TODO: Use custom exception here
                throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
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

        public string GetEmbeddedResourceString(Assembly assembly, string resourceFileName)
        {
            var stream = this.GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        //NOTE: These convenience methods are not available in WinRT, but they're available 
        // in Xamarin.iOS and Xamarin.Android, so i'm commenting them out so they build as
        // a PCL lib, but you may want them in your own code if you're not targeting WinRT.
        //		/// <summary>
        //		/// Attempts to find and return the given resource from within the calling assembly.
        //		/// </summary>
        //		/// <returns>The embedded resource as a stream.</returns>
        //		/// <param name="resourceFileName">Resource file name.</param>
        //		public static Stream GetEmbeddedResourceStream(string resourceFileName)
        //		{
        //			return GetEmbeddedResourceStream (Assembly.GetCallingAssembly (), resourceFileName);
        //		}
        //
        //		/// <summary>
        //		/// Attempts to find and return the given resource from within the calling assembly.
        //		/// </summary>
        //		/// <returns>The embedded resource as a byte array.</returns>
        //		/// <param name="resourceFileName">Resource file name.</param>
        //		public static byte[] GetEmbeddedResourceBytes (string resourceFileName)
        //		{
        //			return GetEmbeddedResourceBytes (Assembly.GetCallingAssembly (), resourceFileName);
        //		}
        //
        //		/// <summary>
        //		/// Attempts to find and return the given resource from within the calling assembly.
        //		/// </summary>
        //		/// <returns>The embedded resource as a string.</returns>
        //		/// <param name="resourceFileName">Resource file name.</param>
        //		public static string GetEmbeddedResourceString (string resourceFileName)
        //		{
        //			return GetEmbeddedResourceString (System.Reflection.Assembly. Assembly.GetCallingAssembly (), resourceFileName);
        //		}
    }
}