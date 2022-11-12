using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Web.Data;

namespace Northwind.Web.Tests
{
    public class IdentityTestHelper
    {
        private readonly UserManager<IdentityUser> userManager;

        public IdentityTestHelper(string? connectionString = null)
        {
            var sc = new ServiceCollection();

            connectionString = string.IsNullOrEmpty(connectionString)
                ? @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true"
                : connectionString;

            sc.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            sc.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var sp = sc.BuildServiceProvider();
            userManager = sp.GetService<UserManager<IdentityUser>>();
        }

        public void DeleteAllUsers()
        {
            foreach (var user in userManager.Users.ToList())
            {
                userManager.DeleteAsync(user).Wait();
            }
        }

        public void AddUser(string email, string password)
        {
            var user = new IdentityUser
            {
                Email = email,
                UserName = email,
            };
            var result = userManager.CreateAsync(user).Result;

            result = userManager.AddPasswordAsync(user, password).Result;
        }
    }
}
