using Microsoft.AspNetCore.Mvc;
using Token_based_Authentication___Authorization.Models;
using Token_based_Authentication___Authorization.Services;

namespace Token_based_Authentication___Authorization.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}