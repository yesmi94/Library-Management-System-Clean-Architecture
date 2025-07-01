namespace LibraryManagementCleanArchitecture.Application.DTO.PersonDTO
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class AddPersonDto
    {
        public string Name { get; set; }
        public UserType Role { get; set; }
        public int BooksBorrowed { get; set; }


    }
}
