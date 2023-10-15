using System;
using System.Linq.Expressions;
using FamilyBudget.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FamilyBudget.Web.Tests.Fixtures
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IConfiguration Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                  .AddJsonFile("integrationsettings.json")
                  .Build();

                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
             //   services.AddTransient<IRepository<Core.Entities.User>, UserRepositoryStub>();
          //     services.AddTransient<IRepository<Core.Entities.Budget>, BudgetRepositoryStub>();
          //      services.AddTransient<IRepository<Core.Entities.BudgetItem>, BudgetItemRepositoryStub>();
            });
        }
    }

    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    { 
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
        }
    }

    public class UserRepositoryStub : IRepository<Core.Entities.User>
    {
        public async Task<Core.Entities.User> Add(Core.Entities.User entity)
        {
            entity.Id = Guid.NewGuid();
            return entity;
        }

        public Task<long> Count(Expression<Func<Core.Entities.User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Entities.User> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Entities.User?> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Core.Entities.User>> GetAll(Expression<Func<Core.Entities.User, bool>> filter = null, Func<IQueryable<Core.Entities.User>, IOrderedQueryable<Core.Entities.User>> orderBy = null, Pageable pageable = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Task<Core.Entities.User> Update(Core.Entities.User entity)
        {
            throw new NotImplementedException();
        }
    }
}

