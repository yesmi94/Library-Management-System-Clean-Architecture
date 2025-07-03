// <copyright file="InvalidPersonException.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Exceptions
{
    public class InvalidPersonException : Exception
    {
        public InvalidPersonException(string message)
            : base(message)
        {
        }
    }
}
