using Assignment_EF_Core_Lec_04.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment_EF_Core_Lec_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Seeding

           using var context = new AppDbContext();

            context.Database.Migrate();

            if (!context.Managers.Any())
            {
                var managers = new List<Manager>
                {
                    new Manager { Id = 1 , FullName = "Ahmed Ali" , Email = "ahmed.ali@example.com" , PhoneNumber = "07676576587" , HireDate =new DateTime(2000, 1, 10) },
                    new Manager { Id = 2 , FullName = "Mohamed Hassan" , Email = "mohamed.hassan@example.com" , PhoneNumber = "04341234132" , HireDate = new DateTime(2010, 5, 15) },
                    new Manager { Id = 3 , FullName = "Sara Ahmed" , Email = "sara.ahmed@example.com" , PhoneNumber = "45354364566" , HireDate = new DateTime(2015, 8, 20) }
                };

                context.Managers.AddRange(managers);
                context.SaveChanges();

            }

            if (!context.Branches.Any())
            {
                var branches = new List<Branch>
                {
                    new Branch { Code = "B001" , Name = "Branch 1" , Address = "Cario", PhoneNumber = "1234567890" , ManagerId = 1 },
                    new Branch { Code = "B002" , Name = "Branch 2" , Address = "Giza" , PhoneNumber = "0987654321" , ManagerId = 2 },
                    new Branch { Code = "B003" , Name = "Branch 3" , Address = "Alex" , PhoneNumber = "0987978665"  , ManagerId = 3 }
                };


                context.Branches.AddRange(branches);
                context.SaveChanges();
            }





            #endregion

            InteractiveMenu.Run(context);

        }
    }
}
