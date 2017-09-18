using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientManagement.Core.Models;

namespace ClientManagement.Core.Interfaces
{
    public interface IUserContext
    {
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string UserId { get; }

        Task AssignUserToRoles(string userId, IEnumerable<UserRole> roles);
        void Dispose();
        IEnumerable<Guid> GetMemberIdsInRole(UserRole role);
        Task<IEnumerable<UserRole>> GetRolesAsync(string userId);
        Task<bool> IsManager();
        Task<bool> IsInRole(string userId, UserRole role);
    }
}