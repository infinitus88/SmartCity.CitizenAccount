using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SmartCity.CitizenAccount.Security.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "SmartCity.CitizenAccountAudience";
        public static string Issuer { get; } = "SmartCity.CitizenAccountIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
        public static string TokenType { get; } = "Bearer";
    }
}
