using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.DTO.PersonDTO
{
    public class AddPersonDto
    {
        public string Name { get; set; }
        public UserType Role { get; set; }
        public int BooksBorrowed { get; set; }


    }
}
