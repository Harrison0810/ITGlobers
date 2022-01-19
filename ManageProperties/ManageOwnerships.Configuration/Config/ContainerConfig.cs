using AutoMapper;
using ManageOwnerships.Domain.Services;
using ManageOwnerships.Infrastructure;
using ManageOwnerships.Infrastructure.Classes;
using ManageOwnerships.Infrastructure.Context;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Entities;
using ManageOwnerships.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageOwnerships.Configuration.Config
{
    public class ContainerConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Add Context
            services.AddDbContext<ManageOwnershipContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), o =>
                {
                    o.EnableRetryOnFailure();
                });
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            // Configuration Repository
            services.AddScoped<IGenericRepository<OwnerEntity, OwnerContract, ManageOwnershipContext>, GenericRepository<OwnerEntity, OwnerContract, ManageOwnershipContext>>();
            services.AddScoped<IGenericRepository<OwnershipEntity, OwnershipContract, ManageOwnershipContext>, GenericRepository<OwnershipEntity, OwnershipContract, ManageOwnershipContext>>();
            services.AddScoped<IGenericRepository<OwnershipImageEntity, OwnershipImageContract, ManageOwnershipContext>, GenericRepository<OwnershipImageEntity, OwnershipImageContract, ManageOwnershipContext>>();
            services.AddScoped<IGenericRepository<OwnershipTraceEntity, OwnershipTraceContract, ManageOwnershipContext>, GenericRepository<OwnershipTraceEntity, OwnershipTraceContract, ManageOwnershipContext>>();

            // Add Interfaces
            services.AddScoped<IDBManageOwnershipsRepository, DBManageOwnershipsRepository>();
            services.AddTransient<IErrorLoggingClient, ErrorLoggingClient>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IOwnershipsService, OwnershipsService>();
            
            // Auto Mapper Configurations
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
