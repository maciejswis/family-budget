using System;
using FamilyBudget.Core.Entities;
using FamilyBudget.Infra;
using Microsoft.EntityFrameworkCore;
using User = FamilyBudget.Core.Entities.User;

namespace FamilyBudget.Web;


public static class SeedData
{
    public static readonly Core.Entities.User user1 = new Core.Entities.User
    {
        UserName = "tom",
        Email = "tom@somedomain.com",
        FullName = "Tom",
        Id = Guid.Parse("3d035ff3-0bb0-4619-a32b-d87c075adf15")
    };
    public static readonly Core.Entities.User user2 = new Core.Entities.User
    {
        UserName = "john",
        Email = "tom@somedomain.com",
        FullName = "John",
        Id = Guid.Parse("3d035ff3-0bb0-4619-a32b-d87c075adf14")
    };

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
            Users = new List<Core.Entities.User> { user1 }
        });

        dbContext.Budgets.Add(new Core.Entities.Budget
        {
            Id = Guid.NewGuid(),
            Name = "Second budget",
            Users = new List<Core.Entities.User> { user1, user2 }
        });

        dbContext.SaveChanges();
    }
}


