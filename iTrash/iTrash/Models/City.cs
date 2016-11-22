using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class City
    {
        [Key]
        public int _ID { get; set; }
        public string _City { get; set; }
        [ForeignKey("state")]
        public int _State { get; set; }
        public State state { get; set; }

    }
}