namespace LibraryManagementCleanArchitecture.Application.DTO.AuthDto
{

    public record RegisterDto
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
