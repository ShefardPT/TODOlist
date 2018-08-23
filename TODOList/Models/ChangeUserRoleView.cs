using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.Models
{
    public class ChangeUserRoleView
    {
        public List<IdentityRole> AllRoles { get; set; }

        public IList<string> UserRoles { get; set}

        public ChangeUserRoleView()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
