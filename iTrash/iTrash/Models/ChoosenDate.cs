using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class ChoosenDate
    {
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}