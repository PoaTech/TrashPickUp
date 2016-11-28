using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Pickup
    {
        [Key]
        public int _ID { get; set; }

        [ForeignKey("user")]
        public string _User { get; set; }
        public ApplicationUser user { get; set; }
    }
}