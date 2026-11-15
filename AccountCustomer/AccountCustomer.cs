using Assignment_EF_Core_Lec_04.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.AccountCustomer
{
    public class AccountCustomer
    {

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OwnerShipStartDate { get; set; }

        [Required]
        public string OwnerShipType { get; set; }

        [Required]
        public string AccountStatus { get; set; }
    }
}
