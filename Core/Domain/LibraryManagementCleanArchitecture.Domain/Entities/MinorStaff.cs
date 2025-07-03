// <copyright file="MinorStaff.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class MinorStaff : Person
    {
        public MinorStaff(string name)
            : base(name, UserType.MinorStaff)
        {
        }

        public override string ShowType() => UserType.MinorStaff.ToString();
    }
}
