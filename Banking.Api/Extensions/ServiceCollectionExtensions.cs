using Banking.Api.Domain;
using Banking.Api.Domain.Interface;
using Banking.Api.Repository;
using Banking.Api.Repository.Interface;
using Banking.Api.UnitOfWork;
using Banking.Api.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerUoW, CustomerUoW>();
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountUoW, AccountUoW>();
        }
    }
}