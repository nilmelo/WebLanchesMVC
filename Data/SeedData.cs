using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebLanchesMVC.Data
{
    public static class SeedData
    {
		public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
		{
			var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			string[] roleNames = { "Admin", "Member" };
			IdentityResult roleResult;

			foreach(var roleName in roleNames)
			{
				var roleExist = await RoleManager.RoleExistsAsync(roleName);
				if(!roleExist)
				{
					roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			var superUser = new IdentityUser {
				UserName = configuration.GetSection("UserSettings")["UserName"],
				Email = configuration.GetSection("UserSettings")["UserEmail"]
			};

			string userPassword = configuration.GetSection("UserSettings")["UserPassword"];

			var user = await UserManager.FindByEmailAsync(configuration.GetSection("UserSettings")["UserEmail"]);

			if(user == null)
			{
				var createSuperUser = await UserManager.CreateAsync(superUser, userPassword);
				if(createSuperUser.Succeeded)
				{
					await UserManager.AddToRoleAsync(superUser, "Admin");
				}
			}

		}

    }
}
