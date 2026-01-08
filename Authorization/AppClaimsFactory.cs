using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using EnglishCenterMVC.Models;
namespace EnglishCenterMVC.Authorization
{
    public class AppClaimsFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public AppClaimsFactory(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("Avatar", user.Avatar ?? ""));
            identity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName ?? ""));
            return identity;
        }
    }
}
