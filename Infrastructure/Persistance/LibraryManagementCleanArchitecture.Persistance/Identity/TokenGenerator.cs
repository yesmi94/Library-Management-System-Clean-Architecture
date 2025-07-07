namespace LibraryManagementCleanArchitecture.Persistance.Identity
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TokenGenerator> logger;

        public TokenGenerator(IConfiguration configuration, ILogger<TokenGenerator> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public string GenerateJwtToken(LoginInfo loginInfo) {

            var roleName = ((UserType)loginInfo.Person.Role).ToString();

            var authInfo = new[]
            {
                new Claim(ClaimTypes.Name, loginInfo.Username),
                new Claim(ClaimTypes.Role, roleName),
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(2),
                claims: authInfo,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
