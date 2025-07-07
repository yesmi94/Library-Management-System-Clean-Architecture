
namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    using LibraryManagementCleanArchitecture.Domain.Entities;

    public interface ITokenGenerator
    {
        string GenerateJwtToken(LoginInfo loginInfo);
    }
}
