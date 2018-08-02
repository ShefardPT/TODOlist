using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API;

namespace TDList.API.Models
{
    public class TDEventDTOtoAdd
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(240)]
        public string Description { get; set; }

        [Required]
        public Status.Importance Importance { get; set; }

        [Required]
        public Status.Urgency Urgency { get; set; }
    }
}
