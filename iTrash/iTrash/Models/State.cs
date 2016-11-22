using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class State
    {
        [Key]
        public int _ID { get; set; }
        public string _State { get; set; }
    }
}