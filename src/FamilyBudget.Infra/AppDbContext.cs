using System.Reflection;
using FamilyBudget.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace FamilyBudget.Infra;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<BudgetItem> BudgetItems => Set<BudgetItem>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Income> Incomes => Set<Income>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Budget>()
           .Property(b => b.RowVersion).IsRowVersion();

        modelBuilder.Entity<Budget>(e => e.HasIndex(i => i.Name).IsUnique());

        modelBuilder.Entity<User>(u => u.HasIndex(i => i.Email).IsUnique());
        modelBuilder.Entity<User>(u => u.HasIndex(i => i.UserName).IsUnique());

        modelBuilder.Entity<Budget>()
         .HasMany(b => b.Users)
         .WithMany();

        modelBuilder.Entity<BudgetItem>()
            .Property(b => b.Value)
            .HasColumnType("SMALLMONEY");

        modelBuilder.Entity<BudgetItem>()
          .Property(b => b.RowVersion).IsRowVersion();

        modelBuilder.Entity<BudgetItem>()
            .Property(b => b.Category)
            .HasConversion<string>();
    }
}

