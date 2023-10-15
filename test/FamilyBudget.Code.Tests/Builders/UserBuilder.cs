using FamilyBudget.Core.Entities;

namespace FamilyBudget.Code.Tests.Builders;

public class UserBuilder
{
    public string FullName { get; private set; } = "Full User Name";
    public string UserName { get; private set; } = "UserName";
    public string Email { get; private set; } = "username@host.com";
    public Guid Id { get; private set; }

    public UserBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public User Build()
    {
        return new User()
        {
            FullName = FullName,
            UserName = UserName,
            Email = Email,
            Id = Id
        };
    }
}