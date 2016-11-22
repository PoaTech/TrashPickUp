using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class PaymentInfo
    {
        [Key]
        public int _ID { get; set; }
        public decimal _Balance { get; set; }
        [ForeignKey("creditCard")]
        public int _CreditCard { get; set; }
        public CreditCard creditCard { get; set; }
        [ForeignKey("checkingAccount")]
        public int _CheckingAccount { get; set; }
        public CheckingAccount checkingAccount { get; set; }
    }
}