using ECommerceWeb.Common;
using Microsoft.AspNetCore.Identity;

namespace ECommerceWeb.WebApi.DataAccess
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<ECommerceUserIdentity>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            //Crear los roles si no existen
            if(!await roleManager.RoleExistsAsync(Constantes.RolAdministrador))
            {
                await roleManager.CreateAsync(new IdentityRole(Constantes.RolAdministrador));
            }
            if (!await roleManager.RoleExistsAsync(Constantes.RolCliente))
            {
                await roleManager.CreateAsync(new IdentityRole(Constantes.RolCliente));
            }

            //Cear usuario administrador si no existe
            var adminUser = await userManager.FindByNameAsync(Constantes.RolAdministrador);
            if(adminUser == null)
            {
                adminUser = new ECommerceUserIdentity
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "999999999",
                    PhoneNumberConfirmed = true,
                    NombreCompleto = "Administrador del Sistema",
                    FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                };

                var result = await userManager.CreateAsync(adminUser,"Pass@123");
                if ((result.Succeeded))
                {
                    await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
                }
            }

        }
    }
}
