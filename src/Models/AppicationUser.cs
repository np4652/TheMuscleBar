using Microsoft.AspNetCore.Identity;
using System;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Models
{
    public class ApplicationUser : ApplicationUserProcModel
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; }
    }


    public class ApplicationUserProcModel : IdentityUser<int>
    {
        //public string UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string AdharNo { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string ReferBy { get; set; }
        public MembershipType MembershipType { get; set; }

    }

    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

    public class UserList
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
