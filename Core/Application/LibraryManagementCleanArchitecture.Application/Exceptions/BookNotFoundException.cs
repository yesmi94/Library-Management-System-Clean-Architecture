﻿namespace LibraryManagementCleanArchitecture.Application.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(string message) : base(message) { }
    }
}
