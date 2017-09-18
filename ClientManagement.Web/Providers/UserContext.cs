using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using ClientManagement.Web;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ClientManagement.Core.Providers
{
    public class UserContext : IUserContext
    {
        private ApplicationUserManager _userManager;

        public string Email
        {
            get
            {
                ValidateContext();
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public string FirstName
        {
            get
            {
                return GetClaimValue("firstName");
            }
        }

        public string LastName
        {
            get
            {
                return GetClaimValue("lastName");
            }
        }

        public string UserId
        {
            get
            {
                return GetClaimValue(ClaimTypes.NameIdentifier);
            }
        }

        public async Task<bool> IsManager()
        {
            return await IsInRole(UserId, UserRole.Manager);
        }

        public IEnumerable<Guid> GetMemberIdsInRole(UserRole role)
        {
            var userMgr = GetUserManager();
            var strRole = role.ToString();

            return userMgr.Users.Where(x => x.Roles.Any(r => r.ToString() == strRole))
                   .Select(y => Guid.Parse(y.Id)).ToList();
        }

        public Task<IEnumerable<UserRole>> GetRolesAsync(string userId)
        {
            var userMgr = GetUserManager();

            return userMgr.GetRolesAsync(userId).ContinueWith(task => {
                return task.Result.Select(x => (UserRole)Enum.Parse(typeof(UserRole), x));
            });
        }

        public async Task AssignUserToRoles(string userId, IEnumerable<UserRole> roles)
        {
            var userMgr = GetUserManager();
            var rolesString = roles.Select(x => x.ToString()).ToArray();

            await userMgr.AddToRolesAsync(userId, rolesString);
        }

        public Task<bool> IsInRole(string userId, UserRole role)
        {
            var userMgr = GetUserManager();

            return userMgr.IsInRoleAsync(userId, role.ToString());
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
        }

        private string GetClaimValue(string claimType)
        {
            ValidateContext();

            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var idClaim = claims.Claims.FirstOrDefault(x => x.Type == claimType);

            return idClaim.Value;
        }

        private void ValidateContext()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Cannot access user data.");
            }
        }

        private ApplicationUserManager GetUserManager()
        {
            if (_userManager == null)
            {
                _userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            return _userManager;
        }
    }
}
