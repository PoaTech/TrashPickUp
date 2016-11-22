using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class CalendarDay
    {
        [Key]
        public int _ID { get; set; }
        public int _Day { get; set; } 
    }
}