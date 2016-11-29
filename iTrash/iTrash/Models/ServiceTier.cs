using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class ServiceTier
    {
        [Key]
        public int _ID { get; set; }
        public string _Tier { get; set; }
    }
}