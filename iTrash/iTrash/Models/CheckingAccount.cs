using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class CheckingAccount
    {
        [Key]
        public int _ID;
        public int _RoutingNumber;
        public int _AccountNumber;
    }
}