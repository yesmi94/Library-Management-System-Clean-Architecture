using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class MinorStaff : Person
    {
        public MinorStaff(string name) : base(name, UserType.MinorStaff) { }

        public override string ShowType() => UserType.MinorStaff.ToString();

    }
}
