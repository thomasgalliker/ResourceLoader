using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Reflection
{
    public interface IResourceLoader
    {
        /// <summary>
        ///     Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource stream.</returns>
        /// <param name="assembly">The assembly which embeds the resource.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName);

        /// <summary>
        ///     Attempts to find and return resources from within the specified assembly that match the given file pattern.
        /// </summary>
        /// <returns>The embedded resource as streams.</returns>
        /// <param name="assembly">The assembly which embeds the resource.</param>
        /// <param name="filePattern">Resource file pattern.</param>
        IEnumerable<Stream> GetEmbeddedResourceStreams(Assembly assembly, string filePattern);

        /// <summary>
        ///     Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a byte array.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        byte[] GetEmbeddedResourceByteArray(Assembly assembly, string resourceFileName);

        /// <summary>
        ///     Attempts to find and return the resources from within the specified assembly that match the given file pattern.
        /// </summary>
        /// <returns>The embedded resources as byte arrays.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        IEnumerable<byte[]> GetEmbeddedResourceByteArrays(Assembly assembly, string resourceFileName);

        /// <summary>
        ///     Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a string.</returns>
        /// <param name="assembly">The assembly which embeds the resource.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        /// <param name="encoding">Character encoding. Default is UTF8.</param>
        string GetEmbeddedResourceString(Assembly assembly, string resourceFileName, Encoding encoding = null);

        /// <summary>
        ///     Attempts to find and return the resources from within the specified assembly that match the given file pattern.
        /// </summary>
        /// <returns>The embedded resources as strings.</returns>
        /// <param name="assembly">The assembly which embeds the resource.</param>
        /// <param name="filePattern">Resource file pattern.</param>
        /// <param name="encoding">Character encoding. Default is UTF8.</param>
        IEnumerable<string> GetEmbeddedResourceStrings(Assembly assembly, string filePattern, Encoding encoding = null);
    }
}