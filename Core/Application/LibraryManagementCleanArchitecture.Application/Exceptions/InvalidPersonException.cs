namespace LibraryManagementCleanArchitecture.Application.Exceptions
{
    public class InvalidPersonException : Exception
    {
        public InvalidPersonException(string message) : base(message) {}
    }
}
