namespace ContosoUniversity.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IUser<int>
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}