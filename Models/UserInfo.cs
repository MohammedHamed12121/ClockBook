using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacebookClone.Enums;

namespace FacebookClone.Models
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string? GivenName { get; set; }
        public string? SurName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; } 
        public DateOnly? DateOfBirth { get; set; } 
        public Genera? Genera { get; set; }

    }
}