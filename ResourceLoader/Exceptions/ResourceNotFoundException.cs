namespace System.Reflection.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceFileName)
            : base(string.Format("Resource ending with {0} not found.", resourceFileName))
        {
        }
    }
}