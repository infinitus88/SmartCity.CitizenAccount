using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class RemoveUsersModel
    {
        public RemoveUsersModel()
        {
            UserIds = new int[] { };
        }
        public int[] UserIds { get; set; }
    }
}
