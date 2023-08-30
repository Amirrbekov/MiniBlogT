using Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data;

public static class DataSeeding
{
    public static void Seed(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<DatabaseContext>();

        context.Database.Migrate();

        if (context.Database.GetPendingMigrations().Count() == 0)
        {
            if (context.Users.Count() == 0)
            {
                User user = new()
                {
                    CreateDate = DateTime.Now,
                    IsActive = true,
                    Name = "Admin",
                    Password = "password",
                    Username = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "1234567890",
                    Surname = "Admin",
                };
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
