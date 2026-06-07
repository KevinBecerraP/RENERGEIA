using Microsoft.AspNetCore.Identity;

namespace RenergeIA.Infrastructure.Identity;

public static class DatabaseSeeder
{
    public static async Task SeedRolesAndAdminAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        foreach (var role in Roles.Todos)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        const string adminEmail = "admin@renergeia.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador del Sistema",
                Cargo = "Administrador",
                Activo = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, Roles.Administrador);
        }
    }
}
