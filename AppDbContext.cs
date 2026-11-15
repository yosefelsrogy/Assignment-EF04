using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04
{
    public class AppDbContext : DbContext
    {
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=NationalBankDB;Trusted_Connection=True;trustservercertificate=true");
        }

        public DbSet<Model.Manager> Managers { get; set; }
        public DbSet<Model.Customer> Customers { get; set; }
        public DbSet<Model.Account> Accounts { get; set; }
        public DbSet<Model.Transaction> Transactions { get; set; }
        public DbSet<Model.Branch> Branches { get; set; }
        public DbSet<AccountCustomer.AccountCustomer> AccountCustomers { get; set; }

    }
}
