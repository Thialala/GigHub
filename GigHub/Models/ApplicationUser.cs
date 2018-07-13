using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Name { get; set; }

        public ICollection<ApplicationUser> Followees { get; set; } = new List<ApplicationUser>();

        public ICollection<ApplicationUser> Followers { get; set; } = new List<ApplicationUser>();
    }
}