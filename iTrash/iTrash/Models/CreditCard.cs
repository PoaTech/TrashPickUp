using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class CreditCard
    {
        [Key]
        public int _ID { get; set; }
        public string _Name { get; set; }
        public string _CardNumber { get; set; }
        [ForeignKey("expirationDate")]
        public int _ExpirationDate { get; set; }
        public ExpirationDate expirationDate { get; set; }
        [ForeignKey("billingAddress")]
        public int _BillingAddress { get; set; }
        public Address billingAddress { get; set; }
        [ForeignKey("cardType")]
        public int _CardType { get; set; }
        public CardType cardType { get; set; }

    }
}