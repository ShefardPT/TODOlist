using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API;

namespace TDList.API.Entities
{
    // Entity of todo-list event
    public class TDEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
