using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Zipcode
    {
        [Key]
        public int ID { get; set; }
        public string _Zipcode { get; set; }
    }
}