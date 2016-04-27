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
        ///     Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a byte array.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        byte[] GetEmbeddedResourceBytes(Assembly assembly, string resourceFileName);

        /// <summary>
        ///     Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a string.</returns>
        /// <param name="assembly">The assembly which embeds the resource.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        /// <param name="encoding">Character encoding. Default is UTF8.</param>
        string GetEmbeddedResourceString(Assembly assembly, string resourceFileName, Encoding encoding = null);
    }
}