using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.Model
{
    public class Customer 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(11)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string CustomerType { get; set; }

        [Required]
        [MaxLength(14)]
        public string NationalId { get; set; }

        public ICollection<AccountCustomer.AccountCustomer> AccountCustomers { get; set; }
    }
}
