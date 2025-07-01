namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class ManagementStaff : Person
    {
        public ManagementStaff(string name) : base(name, UserType.ManagementStaff) { }

        public List<Book> DisplayBooks(List<Book> books) => books;

        public override string ShowType() => UserType.ManagementStaff.ToString();

    }
}
