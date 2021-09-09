using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Security
{
    public interface ISecurityContext
    {
        User User { get; }
        
        bool IsAdministrator { get; }
    }
}
