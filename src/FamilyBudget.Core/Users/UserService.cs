using System;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Users
{
	public interface IUserService
	{
        Task<UserDto?> GetByIdAsync(Guid userId);
    }

	public class UserService: IUserService
	{
		private readonly IRepository<User> _repository;

		public UserService(IRepository<User> repository)
		{
			_repository = repository;
		}

		public async Task<UserDto?> GetByIdAsync(Guid userId)
		{
			var user = await _repository.GetByIdAsync(userId);
			if (user == null) { return null; }
			return new UserDto { Name = user.Name, Id = user.Id };
		}
	}

	public class UserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}

