﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class CreateCitizenModel
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public byte Sex { get; set; }
        public string DateOfBirth { get; set; }
    }
}
