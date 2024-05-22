using Microsoft.AspNetCore.Identity;

namespace WebApplication14.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
