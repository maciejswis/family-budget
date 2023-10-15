namespace FamilyBudget.Core.Entities;

public class Budget: IEntity
{
    public Budget() { }
     
    public byte[] RowVersion { get; set; }

    public Budget(string name, User user)
    {
        Name = name;
        Users.Add(user);
        // CreatedBy = user.Id;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();
    public ICollection<User> Users { get; set; } = new List<User>();
    // public Guid CreatedBy { get; set; }
}

