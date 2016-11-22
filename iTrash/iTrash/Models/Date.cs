using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Date
    {
        [Key]
        public int _ID { get; set; }
        [ForeignKey("day")]
        public int Day { get; set; }
        public CalendarDay day { get; set; }
        [ForeignKey("month")]
        public int Month { get; set; }
        public Month month { get; set; }
        [ForeignKey("year")]
        public int Year { get; set; }
        public Year year { get; set; }
    }
}