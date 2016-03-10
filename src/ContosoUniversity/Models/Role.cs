namespace ContosoUniversity.Models
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role : IRole<int>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}