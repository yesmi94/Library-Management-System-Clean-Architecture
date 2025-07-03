// <copyright file="Person.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public abstract class Person
    {
        public string Id { get; init; } = Guid.NewGuid().ToString("N");

        public string Name { get; init; }

        public UserType Role { get; init; }

        public int BorrowedBooksNum { get; set; }

        public Person(string name, UserType role)
        {
            this.Name = name;
            this.Role = role;
        }

        public abstract string ShowType();
    }
}
