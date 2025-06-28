using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.DTO.PersonDTO
{
    public class PersonDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public UserType Role { get; set; }
        public int BooksBorrowed { get; set; }

    }
}
