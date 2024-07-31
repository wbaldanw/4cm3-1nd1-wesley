using Acme.TechnicalTest.Domain.Contracts.UserManagement;
using Acme.TechnicalTest.Domain.Domain.Shared;
using Acme.TechnicalTest.Domain.Domain.UserManagement;
using Acme.TechnicalTest.Domain.DTO.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Acme.TechnicalTest.Application.UseCases.UserManagement
{
    public class SigninUC : ISigninUC
    {
        private readonly IUserRepository userRepository;                
        private readonly SignInManager<User> signInManager;        
        private readonly IConfigurationSection jwtConfiguration;

        public SigninUC(IUserRepository userRepository,            
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;            
            this.jwtConfiguration = configuration.GetSection("JWT");
        }

        public async Task<SignInResponse> SignIn(SignInRequest loginDTO)
        {
            var user = await userRepository.GetByEmail(loginDTO.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid user or email");

            var result = await signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, true, lockoutOnFailure: true);

            if (result.IsLockedOut)
                throw new UnauthorizedAccessException("Account temporary blocked.");

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid password or user");

            var token = GenerateToken(user);            

            return new SignInResponse()
            {
                Token = token.Token,
                Expiration = token.Expiration,                
            };
        }

        private TokenResultDTO GenerateToken(User user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName.ToString()),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(CustomClaimTypes.TenantId, user.TenantId.ToString())
                };

            if (user.Tenant?.Name == "Acme")
                authClaims.Add(new Claim(ClaimsForPolicy.TenantManagement, "writer"));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration["Secret"].ToString()));

            var token = new JwtSecurityToken(
                issuer: jwtConfiguration["ValidIssuer"].ToString(),
                audience: jwtConfiguration["ValidAudience"].ToString(),
                expires: DateTime.Now.AddHours(36),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);            

            return new TokenResultDTO
            {
                Token = jwtToken,
                Expiration = token.ValidTo,                
            };
        }        
    }
}
