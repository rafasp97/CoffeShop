using DotNetEnv;
using Streamline.Infrastructure.Persistence.SqlServer.DbContexts;
using Streamline.Application.Repositories;
using Streamline.Infrastructure.Persistence.SqlServer.Repositories;
using Streamline.Application.Customers.CreateCustomer;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;

namespace Streamline.API.Factory
{
    public static class AppFactory
    {
        public static WebApplication CreateApp(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            var sqlConnection = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION");

            builder.Services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(sqlConnection));

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly);
            });

            builder.Services.AddAutoMapper(typeof(AppFactory));

            var app = builder.Build();
            return app;
        }
    }
}
