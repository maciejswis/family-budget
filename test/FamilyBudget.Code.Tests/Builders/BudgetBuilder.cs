using FamilyBudget.Core.Entities;

namespace FamilyBudget.Code.Tests.Builders;

public class BudgetBuilder
{
    private Guid Id;
    public string Name { get; private set; } = "budget name";
    public ICollection<User> Users { get; private set; } = new List<User>();
    public ICollection<BudgetItem> BudgetItems { get; private set; } = new List<BudgetItem>();

    public BudgetBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public Budget Build()
    {
        return new Budget
        {
            Id = this.Id,
            Name = this.Name,
            Users = this.Users,
            BudgetItems = this.BudgetItems
        };
    }
}
