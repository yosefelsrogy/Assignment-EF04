using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.Model
{
    public class Branch 
    {
        [Key]
        [MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(11)]
        [Phone]
        public string PhoneNumber { get; set; }


        [ForeignKey("Manager")]
        public int ManagerId { get; set; }  
        public Manager Manager { get; set; }

        public ICollection<Account> Accounts { get; set; }

    }
}
