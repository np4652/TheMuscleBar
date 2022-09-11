﻿namespace TheMuscleBar.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }


        public AuthenticateResponse(ApplicationUser user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Role = user.Role;
            Name=user.Name;
            RefreshToken = user.RefreshToken;
            Token = token;
        }
    }
}