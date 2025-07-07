namespace LibraryManagementCleanArchitecture.Application.UseCases.Login
{
    using LibraryManagementCleanArchitecture.Application.DTO.AuthDto;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class PersonLoginCommandHandler : IRequestHandler<PersonLoginCommand, Result<LoginResponseDto>>
    {

        private readonly IRepository<LoginInfo> loginRepository;
        private readonly ITokenGenerator tokenGenerator;

        public PersonLoginCommandHandler(IRepository<LoginInfo> loginRepository, ITokenGenerator tokenGenerator)
        {
            this.loginRepository = loginRepository;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Result<LoginResponseDto>> Handle(PersonLoginCommand request, CancellationToken cancellationToken)
        {

            var user = await this.loginRepository.GetByUsernameAsync(request.username);

            if (user == null)
            {
                return Result<LoginResponseDto>.Failure("Failed: Unable to find the user");
            }

            var token = this.tokenGenerator.GenerateJwtToken(user);

            var loginResponseDto = new LoginResponseDto { Token = token};

            return Result<LoginResponseDto>.Success(loginResponseDto);
        }
    }
}
