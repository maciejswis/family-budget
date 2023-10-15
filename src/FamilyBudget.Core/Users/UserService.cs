using System;
using System.Linq.Expressions;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Users
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(Guid userId);

        Task<UserDto> Add(UserDto userDto);

        Task<PagedResponse<UserDto>> GetAll(
            string? userNamePrefix = null,
            Pageable? pageable = null
            );
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<UserDto>> GetAll(
            string? userNamePrefix = null,
            Pageable? pageable = null)
        {
            Expression<Func<User, bool>>? filter = !string.IsNullOrEmpty(userNamePrefix) ?
                (u) => u.UserName.StartsWith(userNamePrefix) : null;

            var results = await _repository.GetAll(
                filter,
                pageable?.GetOrderSpec<User>(u => u.UserName),
                pageable);
            var total = await _repository.Count(filter);

            return new PagedResponse<UserDto>
            {
                Results = results.Select(MapToDto).ToList(),
                Total = total,
            };
        }

        public async Task<UserDto?> GetByIdAsync(Guid userId)
        {
            var user = await _repository.Get(userId);
            if (user == null) { return null; }
            return MapToDto(user);
        }

        public async Task<UserDto> Add(UserDto userDto)
        {
            var user = new User()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName,
            };
            var result = await _repository.Add(user);
            return MapToDto(result);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email
            };
        }

    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

