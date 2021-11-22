using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerHost.Quickstart.UI;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class RetailProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.Claims.First(c => c.Type == "sub").Value;

            var user = TestUsers.Users.First(u => u.SubjectId == userId);

            var extraClaims = new List<Claim>
            {
                new Claim("retail.user_id", userId),
                new Claim("retail.user_email", user.Claims.First(x => x.Type == JwtClaimTypes.Email).Value),
            };

            context.IssuedClaims.AddRange(extraClaims);

            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.Claims.First(c => c.Type == "sub").Value;

            var user = TestUsers.Users.First(u => u.SubjectId == userId);
            context.IsActive = (user != null) && user.IsActive;

            await Task.CompletedTask;
        }
    }
}
