using Microsoft.IdentityModel.Tokens;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SmartCity.CitizenAccount.Security.Auth
{
    public class TokenBuilder : ITokenBuilder
    {
        public string Build(string name, string role, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, role));

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(name, "Bearer"),
                claims
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }

        public string GetUniqueNameByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)handler.ReadToken(token);
            if (securityToken == null)
            {
                throw new BadRequestException("Token is not valid!");
            }
            var uniqueName = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value; var tokenHanlder = new JwtSecurityTokenHandler();

            return uniqueName;
        }
    }
}
