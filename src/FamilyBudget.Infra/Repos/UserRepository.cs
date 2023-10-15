using FamilyBudget.Core.Entities;

namespace FamilyBudget.Infra
{

    public class UserRepository : EfCoreRepository<User, AppDbContext>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}