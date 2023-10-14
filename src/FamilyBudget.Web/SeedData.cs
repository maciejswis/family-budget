using System;
using FamilyBudget.Core.Entities;
using FamilyBudget.Infra;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Web;


public static class SeedData
{
    public static readonly User user1 = new User { Name = "Maciej", Id = Guid.NewGuid() };
    public static readonly User user2 = new User { Name = "Tomek", Id = Guid.NewGuid() };

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            // Look for any Users.
            if (dbContext.Users.Any())
            {
                return;   // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Users)
        {
            dbContext.Remove(item);
        }
        dbContext.SaveChanges();

        dbContext.Users.Add(user1);
        dbContext.Users.Add(user2);

        dbContext.Budgets.Add(new Core.Entities.Budget
        {
            Id = Guid.NewGuid(),
            Name = "First budget",
            Users = new List<User> { user1 }
        });

        dbContext.Budgets.Add(new Core.Entities.Budget
        {
            Id = Guid.NewGuid(),
            Name = "First budget",
            Users = new List<User> { user1, user2 }
        });

        dbContext.SaveChanges();
    }
}


