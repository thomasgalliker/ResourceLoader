namespace System.Reflection.Exceptions
{
    public class MultipleResourcesFoundException : Exception
    {
        public MultipleResourcesFoundException(string resourceFileName, string[] resourcePaths)
            : base(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)))
        {
        }
    }
}