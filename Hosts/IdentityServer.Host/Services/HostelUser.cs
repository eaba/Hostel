using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Host.Services
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class HostelUser : IdentityUser
    {
        public string Hostel { get; set; }
    }
}
