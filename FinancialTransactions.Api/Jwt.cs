using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialTransactions.Api
{
    public class Jwt
    {
        public string SecurityKey { get; set; }

        public static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection(nameof(Jwt));
            services.Configure<Jwt>(jwtSection);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetSymmetricSecurityKey(jwtSection.Get<Jwt>().SecurityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public string GenerateToken(int authenticatedUserId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //more info in https://balta.io/artigos/aspnet-5-autenticacao-autorizacao-bearer-jwt#autorizando
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, authenticatedUserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(SecurityKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            var key = Encoding.ASCII.GetBytes(securityKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
