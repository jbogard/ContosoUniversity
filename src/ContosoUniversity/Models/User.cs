namespace ContosoUniversity.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class User : IUser<int>
    {
        private string _userName;
        private string _passwordHash;
        private string _email;
        private int _accessFailedCount;

        private User() { }

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
            EmailConfirmed = true;
            LockoutEnabled = false;
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
            Roles = new HashSet<Role>();
        }

        public int Id { get; private set; }

        [StringLength(50, MinimumLength = 3)]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _userName = value;
            }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _passwordHash = value;
            }
        }

        [StringLength(50, MinimumLength = 3)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _email = value;
            }
        }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount
        {
            get { return _accessFailedCount; }
            set
            {
                if (_accessFailedCount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The access failed count should be greater than zero.");
                }

                _accessFailedCount = value;
            }
        }

        public DateTime? LockoutEndDateUtc { get; set; }

        public ICollection<Role> Roles { get; set; }

        public void AddToRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (Roles.Any(r => r.Id == role.Id))
            {
                throw new InvalidOperationException("The user is already in the role.");
            }

            Roles.Add(role);
        }

        public bool IsInRole(string roleName)
        {
            return Roles.Any(r => r.Name == roleName);
        }

        public void RemoveFromRole(string roleName)
        {
            var role = Roles.SingleOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                throw new InvalidOperationException("The user is not in the role.");
            }

            Roles.Remove(role);
        }
    }
}