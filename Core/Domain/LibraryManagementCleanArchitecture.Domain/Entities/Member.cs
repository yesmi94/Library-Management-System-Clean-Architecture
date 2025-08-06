// <copyright file="Member.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class Member : Person
    {
        private int borrowedBooksNum = 0;

        public Member(string name)
            : base(name, UserType.Member)
        {
        }

        public new int BorrowedBooksNum
        {
            get { return this.borrowedBooksNum; }
            set { this.borrowedBooksNum = value; }
        }

        public override string ShowType() => UserType.Member.ToString();
    }
}
