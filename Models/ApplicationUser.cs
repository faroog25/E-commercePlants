using Microsoft.AspNetCore.Identity;

namespace E_commercePlants.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
} 