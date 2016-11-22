using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class Date
    {
        [Key]
        public int ID;
        public int Day;
        public int Month;
        public int Year;
    }
}