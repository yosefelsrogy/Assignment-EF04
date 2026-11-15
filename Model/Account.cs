using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int AccountNumber { get; set; }

        [Required]
        public decimal CurrentBalance { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OpeningDate { get; set; }

        [ForeignKey("Branches")]
        public string BranchCode { get; set; }
        public Branch Branches { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<AccountCustomer.AccountCustomer> AccountCustomers { get; set; }
    }
}
