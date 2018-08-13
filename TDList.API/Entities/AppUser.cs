using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Entities
{
    // Entity of application user
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(63)]
        public string Name { get; set; }

        public string Role { get; set; }
    }
}
