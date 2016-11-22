using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Truck
    {
        [Key]
        public int _ID { get; set; }
        public string _TruckNumber { get; set; }
        [ForeignKey("Zipcode")]
        public int _Zipcode { get; set; }
    }
}