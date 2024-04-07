using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser:IdentityUser
    {
        // dodaje tutaj dodatkowe wartosci
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
