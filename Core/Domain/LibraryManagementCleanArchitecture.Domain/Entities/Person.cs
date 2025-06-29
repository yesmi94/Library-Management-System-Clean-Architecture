using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;


namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public abstract class Person
    {

        public string Id { get; init; } = Guid.NewGuid().ToString("N");
        public string Name { get; init; }
        public UserType Role { get; init; }
        public int BorrowedBooksNum { get; set; }

        public Person(string name, UserType role)
        {
            Name = name;
            Role = role;
        }

        public abstract string ShowType();
    }
}

