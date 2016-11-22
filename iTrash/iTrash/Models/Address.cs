using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Address
    {
        [Key]
        public int _ID { get; set; }
        public string _StreetAddress1 { get; set; }
        public string _StreetAddress2 { get; set; }
        [ForeignKey("city")]
        public int _City { get; set; }
        public City city { get; set; }
        [ForeignKey("zipcode")]
        public int _Zipcode { get; set; }
        public Zipcode zipcode { get; set; }
    }
}