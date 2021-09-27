using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class GetCitizenList
    {
        public GetCitizenList()
        {
            Data = new List<CitizenListItem>();
        }
        public IList<CitizenListItem> Data { get; set; }
    }

    public class CitizenListItem
    {
        public string Value { get; set; }
        public string PhotoUrl { get; set; }
        public string FullName { get; set; }
    }
}
