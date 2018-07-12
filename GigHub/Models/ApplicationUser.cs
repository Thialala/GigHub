using Microsoft.AspNetCore.Identity;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Name { get; set; }
    }
}