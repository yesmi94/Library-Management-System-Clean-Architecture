namespace LibraryManagementCleanArchitecture.Application.UseCases.Login
{
    using LibraryManagementCleanArchitecture.Application.DTO.AuthDto;
    using MediatR;

    public record PersonLoginCommand(string username, string password) : IRequest<Result<LoginResponseDto>>;
}
