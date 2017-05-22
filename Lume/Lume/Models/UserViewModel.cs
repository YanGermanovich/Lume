using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lume.Models
{

    public enum UserRole
    {
        User,
        Company
    }

    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public long Id_City { get; set; }
        public string PhoneNumber { get; set; }
        public long Id_Avatar { get; set; }
        public double? N { get; set; }
        public double? E { get; set; }
        public bool RememberMe { get; set; }
    }
}