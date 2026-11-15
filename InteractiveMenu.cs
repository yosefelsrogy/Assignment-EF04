using Assignment_EF_Core_Lec_04.AccountCustomer;
using Assignment_EF_Core_Lec_04.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EF_Core_Lec_04
{
    public class InteractiveMenu
    {
        public static void Run(AppDbContext context)
        {
            while (true)
            {
                Console.WriteLine("================================");
                Console.WriteLine("National Bank - Management");
                Console.WriteLine("================================");
                Console.WriteLine("1) Add A New Customer");
                Console.WriteLine("2) Open A New Account For A Customer");
                Console.WriteLine("3) Update Account Status (Active / Closed)");
                Console.WriteLine("4) Remove An Account From A Customer");
                Console.WriteLine("5) List All Customers (With Accounts)");
                Console.WriteLine("0) Exit");
                Console.WriteLine("---------------------------------");
                Console.Write("Enter Choice: ");
                var choice = Console.ReadLine();

                if (choice == "0")
                    break;

                switch (choice)
                {
                    case "1": AddCustomer(context); break;
                    case "2": OpenNewAccount(context); break;
                    case "3": UpdateStatus(context); break;
                    case "4": RemoveAccount(context); break;
                    case "5": ListCustomers(context); break;
                    default: Console.WriteLine("Invalid choice, please try again."); break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey(true);
            }
        }
            

        #region Option 1:
        public static void AddCustomer(AppDbContext context)
        {
            Console.WriteLine("--- Add New Customer ---");

            Console.Write("Full Name      : ");
            string fullName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(fullName) || fullName.Length > 100)
            {
                Console.WriteLine("Name is required (max 100 characters).");
                Console.Write("Full Name      : ");
                fullName = Console.ReadLine();
            }

            Console.Write("National ID    : ");
            string nationalId = Console.ReadLine();
            while (nationalId == null || nationalId.Length != 14 || !nationalId.All(char.IsDigit))
            {
                Console.WriteLine("National ID must be exactly 14 digits.");
                Console.Write("National ID    : ");
                nationalId = Console.ReadLine();
            }

            Console.Write("Date of Birth  : (yyyy-MM-dd) ");
            DateTime dob;
            while (!DateTime.TryParse(Console.ReadLine(), out dob) || dob >= DateTime.Today)
            {
                Console.WriteLine("Invalid date. Use the format yyyy-MM-dd.");
                Console.Write("Date of Birth  : (yyyy-MM-dd) ");
            }

            Console.Write("Email          : ");
            string email = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Invalid email.");
                Console.Write("Email          : ");
                email = Console.ReadLine();
            }

            Console.Write("Phone          : ");
            string phone = Console.ReadLine();
            while (phone == null || (phone.Length != 10 && phone.Length != 11) || !phone.All(char.IsDigit))
            {
                Console.WriteLine("Phone must be 10 or 11 digits.");
                Console.Write("Phone          : ");
                phone = Console.ReadLine();
            }

            Console.Write("Address        : ");
            string address = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(address) || address.Length > 100)
            {
                Console.WriteLine("Address is required (max 100 characters).");
                Console.Write("Address        : ");
                address = Console.ReadLine();
            }

            Console.WriteLine("Customer Type:");
            Console.WriteLine("    1) Individual");
            Console.WriteLine("    2) Business");
            Console.Write("  Choice: ");
            int typeChoice;
            while (!int.TryParse(Console.ReadLine(), out typeChoice) || typeChoice < 1 || typeChoice > 2)
            {
                Console.WriteLine("Please enter 1 or 2.");
                Console.Write("  Choice: ");
            }
            string type = typeChoice == 1 ? "Individual" : "Business";

            int newId = context.Customers.Any() ? context.Customers.Max(c => c.Id) + 1 : 1;

            var customer = new Customer
            {
                Id = newId,
                FullName = fullName,
                NationalId = nationalId,
                DateOfBirth = dob,
                Email = email,
                PhoneNumber = phone,
                Address = address,
                CustomerType = type
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Customer created successfully. CustomerId = {newId}");
            Console.ResetColor();
        }
        #endregion

        #region Option 2:

        public static void OpenNewAccount(AppDbContext context)
        {
            Console.WriteLine("--- Open New Account ---");

            Console.Write("Account Number : ");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber) || accountNumber <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Account Number : ");
            }

            Console.WriteLine("Account Type:");
            Console.WriteLine("    1) Savings");
            Console.WriteLine("    2) Current");
            Console.WriteLine("    3) Business");
            Console.Write("  Choice: ");
            int typeChoice;
            while (!int.TryParse(Console.ReadLine(), out typeChoice) || typeChoice < 1 || typeChoice > 3)
            {
                Console.WriteLine("Please enter 1, 2 or 3.");
                Console.Write("  Choice: ");
            }
            string accountType = typeChoice == 1 ? "Savings" : typeChoice == 2 ? "Current" : "Business";

            Console.Write("Branch Code    : ");
            string branchCode = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(branchCode))
            {
                Console.WriteLine("Branch code is required.");
                Console.Write("Branch Code    : ");
                branchCode = Console.ReadLine();
            }
            branchCode = branchCode.Trim();

            Console.Write("Customer Id    : ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Customer Id    : ");
            }

            Console.WriteLine("Ownership Role:");
            Console.WriteLine("    1) Primary");
            Console.WriteLine("    2) CoHolder");
            Console.Write("  Choice: ");
            int roleChoice;
            while (!int.TryParse(Console.ReadLine(), out roleChoice) || roleChoice < 1 || roleChoice > 2)
            {
                Console.WriteLine("Please enter 1 or 2.");
                Console.Write("  Choice: ");
            }
            string role = roleChoice == 1 ? "Primary" : "CoHolder";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Validating branch '{branchCode}' and customer #{customerId}...");
            Console.ResetColor();

            var branch = context.Branches.Find(branchCode);
            if (branch == null)
            {
                Console.WriteLine($"Branch '{branchCode}' does not exist.");
                return;
            }

            var customer = context.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer #{customerId} does not exist.");
                return;
            }

            var account = context.Accounts.Find(accountNumber);
            if (account == null)
            {
                account = new Account
                {
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    BranchCode = branch.Code,
                    CurrentBalance = 0,
                    OpeningDate = DateTime.Today
                };
                context.Accounts.Add(account);
            }
            else if (context.AccountCustomers.Any(x => x.AccountNumber == accountNumber && x.CustomerId == customerId))
            {
                Console.WriteLine("This customer is already linked to this account.");
                return;
            }

            context.AccountCustomers.Add(new AccountCustomer.AccountCustomer
            {
                AccountNumber = accountNumber,
                CustomerId = customerId,
                OwnerShipStartDate = DateTime.Today,
                OwnerShipType = role,
                AccountStatus = "Active"
            });

            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Account '{accountNumber}' created and linked to customer {customerId} as {role} owner.");
            Console.ResetColor();
        }
        #endregion

        #region Option 3:
        public static void UpdateStatus(AppDbContext context)
        {
            Console.WriteLine("--- Update Account Status ---");

            Console.Write("Account Number : ");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber) || accountNumber <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Account Number : ");
            }

            Console.Write("Customer Id    : ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Customer Id    : ");
            }

            var link = context.AccountCustomers.Find(accountNumber, customerId);
            if (link == null)
            {
                Console.WriteLine("No link found between this account and this customer.");
                return;
            }

            Console.WriteLine("New Status:");
            Console.WriteLine("    1) Active");
            Console.WriteLine("    2) Closed");
            Console.Write("  Choice: ");
            int statusChoice;
            while (!int.TryParse(Console.ReadLine(), out statusChoice) || statusChoice < 1 || statusChoice > 2)
            {
                Console.WriteLine("Please enter 1 or 2.");
                Console.Write("  Choice: ");
            }

            link.AccountStatus = statusChoice == 1 ? "Active" : "Closed";
            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Status updated to {link.AccountStatus}.");
            Console.ResetColor();
        }
        #endregion

        #region Option 4:
        public static void RemoveAccount(AppDbContext context)
        {
            Console.WriteLine("--- Remove Account From Customer ---");

            Console.Write("Account Number : ");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber) || accountNumber <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Account Number : ");
            }

            Console.Write("Customer Id    : ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write("Customer Id    : ");
            }

            var link = context.AccountCustomers.Find(accountNumber, customerId);
            if (link == null)
            {
                Console.WriteLine("No link found between this account and this customer.");
                return;
            }

            context.AccountCustomers.Remove(link);

            // is there any other owner left on this account?
            bool hasOtherOwners = context.AccountCustomers
                .Any(x => x.AccountNumber == accountNumber && x.CustomerId != customerId);

            if (!hasOtherOwners)
            {
                var account = context.Accounts.Find(accountNumber);
                context.Accounts.Remove(account);
            }

            context.SaveChanges();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Ownership link deleted.");
            Console.ResetColor();

            if (!hasOtherOwners)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"      That was the last owner - account '{accountNumber}' was also removed.");
                Console.ResetColor();
            }
        }
        #endregion

        #region Option 5:
        public static void ListCustomers(AppDbContext context)
        {
            Console.WriteLine("--- All Customers ---");
            Console.WriteLine();

            var customers = context.Customers
                .Include(c => c.AccountCustomers)
                    .ThenInclude(ac => ac.Account)
                        .ThenInclude(a => a.Branches)
                .ToList();

            foreach (var c in customers)
            {
                Console.WriteLine($"  #{c.Id} {c.FullName} ({c.CustomerType})");

                if (c.AccountCustomers.Count == 0)
                {
                    Console.WriteLine("      (no accounts)");
                    continue;
                }

                foreach (var ac in c.AccountCustomers)
                {
                    Console.WriteLine($"      {ac.Account.AccountNumber,-8} {ac.Account.AccountType,-9} " +
                        $"Balance: {ac.Account.CurrentBalance,12:N2}   {ac.OwnerShipType,-8} {ac.AccountStatus,-7} " +
                        $"@ {ac.Account.Branches.Name}");
                }
            }
        }

        #endregion
    }

}


        
