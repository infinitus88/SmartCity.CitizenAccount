using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models.Constants
{
    public static class Services
    {
        private static IReadOnlyDictionary<int, ServiceDetail> _services
            = new Dictionary<int, ServiceDetail>
            {
                { 1, new ServiceDetail { Name = "City Administration", Email = "city-administration@email.com" } },
                { 2, new ServiceDetail { Name = "School", Email = "schoold@email.com" } },
                { 3, new ServiceDetail { Name = "Catering", Email = "catering@email.com" } },
                { 4, new ServiceDetail { Name = "Housing", Email = "housing@email.com" } },
                { 5, new ServiceDetail { Name = "Sport Complex", Email = "sport-complex@email.com"} }
            };

        public static IReadOnlyDictionary<int, ServiceDetail> Get => _services;
    }

    public struct ServiceDetail
    {
        public string Email;
        public string Name;
    }
}
