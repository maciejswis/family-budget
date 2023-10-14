using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Core.Entities;

 
public abstract class BudgetItem: IEntity
{
    public BudgetItem() { }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    // public Money Expences { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } 
}

public class Expense: BudgetItem
{
    public Expense() { }
  //  public BudgetType Type { get; private set; } = BudgetType.Expense;
}

public class Income: BudgetItem
{
    public Income()
    {

    }
  //    public BudgetType Type { get; private set; } = BudgetType.Income;
}

public enum BudgetType
{
    Unknown = 0,
    Income = 1,
    Expense = 2,
}

public class Money
{
    public Currency Currency { get; set; }
    public decimal Value { get; set; }
}

public enum Currency
{
    PLN = 0,
    USD = 1,
}

