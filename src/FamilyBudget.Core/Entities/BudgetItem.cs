namespace FamilyBudget.Core.Entities;


public abstract class BudgetItem : IEntity
{
    public BudgetItem() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public Guid BudgetId { get; set; }
    public byte[] RowVersion { get; set; }
    public Currency Currency { get; set; }
    public BudgetItemCategory Category { get; set; }
}

public class Expense : BudgetItem
{
    public Expense() { }
}

public class Income : BudgetItem
{
    public Income() { }
}

public enum BudgetType : int
{
    Unknown = 0,
    Income = 1,
    Expense = 2,
}

public enum Currency
{
    PLN = 0,
    USD = 1,
}

public enum BudgetItemCategory : int
{
    Other = 0,
    Housing = 1,
    Transportation = 2,
    Food = 3,
    Utilities = 4,
    Insurance = 5,
    MedicalHealthcare = 6,
    Saving = 7,
    PersonalSpending = 8
}

