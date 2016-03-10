namespace ContosoUniversity.DAL
{
    using ContosoUniversity.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserStore : IUserStore<User, int>, IUserLockoutStore<User, int>, IUserPasswordStore<User, int>, IUserTwoFactorStore<User, int>, IUserRoleStore<User, int>
    {
        private readonly SchoolContext _dbContext;

        public UserStore(SchoolContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToRoleAsync(User user, string roleName)
        {
            var role = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == roleName);

            if (role == null)
            {
                throw new ArgumentException("roleName");
            }

            user.Roles.Add(role);
        }

        public Task CreateAsync(User user)
        {
            _dbContext.Users.Add(user);        
            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _dbContext.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _dbContext.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            DateTimeOffset dateTimeOffset;

            if (user.LockoutEndDateUtc.HasValue)
            {
                var lockoutEndDateUtc = user.LockoutEndDateUtc;
                dateTimeOffset = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            else
            {
                dateTimeOffset = new DateTimeOffset();
            }

            return Task.FromResult(dateTimeOffset);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult<IList<string>>(user.Roles.Select(r => r.Name).ToList());
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(r => r.Name == roleName));
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var role = user.Roles.SingleOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                throw new ArgumentException("roleName");
            }

            user.Roles.Remove(role);
            return Task.FromResult<object>(null);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            var nullable = lockoutEnd == DateTimeOffset.MinValue ? null : new DateTime?(lockoutEnd.UtcDateTime);
            user.LockoutEndDateUtc = nullable;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            return Task.FromResult<object>(null);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }
    }
}