using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int TransactionNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Note { get; set; }

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public Account Account { get; set; }
    }
}
