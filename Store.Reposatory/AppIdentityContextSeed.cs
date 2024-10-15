using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) {
                var user = new AppUser
                {
                    DisplayName = "Alaa Hafez",
                    Email ="alaa@gmail.com",
                    UserName="AlaaMohamed",
                    Address=new Address
                    {
                       FirstName="alaa",
                       LastName="hafez",
                       City="itay",
                       State="alex",
                       Street="222",
                       ZipCode="12345"
                    }
                };
                await userManager.CreateAsync(user,"password123!");
            }
        }
    }
}
