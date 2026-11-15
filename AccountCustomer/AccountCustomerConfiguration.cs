using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04.AccountCustomer
{
    internal class AccountCustomerConfiguration : IEntityTypeConfiguration<AccountCustomer>
    {
        public void Configure(EntityTypeBuilder<AccountCustomer> AC)
        {
            AC.HasKey(AC => new { AC.AccountNumber, AC.CustomerId });
        }
    }
}
