using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName="Esraa",
                    Email="Esraa@gmail.com",
                    UserName="esraa",
                    Address=new Address
                    {
                        FirstName="esraa",
                        LastName="adel",
                        City="cairo",
                        State= "cairo",
                        Street="3",
                        PostCode="12345"
                    }
                };
                await userManager.CreateAsync(user,"Password123!");
            }
        }

    }
}
