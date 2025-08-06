// <copyright file="BookNotFoundException.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(string message)
            : base(message)
        {
        }
    }
}
