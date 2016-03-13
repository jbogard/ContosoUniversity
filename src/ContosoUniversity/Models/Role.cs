namespace ContosoUniversity.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role : IRole<int>
    {
        private string _name;

        private Role() { }

        public Role(string name)
        {
            Name = name;
            Users = new HashSet<User>();
        }

        public int Id { get; private set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _name = value;
            }
        }

        public ICollection<User> Users { get; set; }
    }
}