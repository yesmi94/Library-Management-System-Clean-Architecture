namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class LoginInfo
    {
        public string LoginId { get; init; } = Guid.NewGuid().ToString("N");

        public string PersonId { get; init; } = Guid.NewGuid().ToString("N");

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }
}
