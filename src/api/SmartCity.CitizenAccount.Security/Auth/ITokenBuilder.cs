using System;

namespace SmartCity.CitizenAccount.Security.Auth
{
    public interface ITokenBuilder
    {
        string Build(string name, string role, DateTime expireDate);
        string GetUniqueNameByToken(string token);
    }
}
