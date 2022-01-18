using AutoMapper;
using ManageProperties.Domain.Services;
using ManageProperties.Infrastructure;
using ManageProperties.Infrastructure.Classes;
using ManageProperties.Infrastructure.Context;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Entities;
using ManageProperties.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageProperties.Configuration.Config
{
    public class ContainerConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Add Context
            services.AddDbContext<ManagePropertiesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), o =>
                {
                    o.EnableRetryOnFailure();
                });
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            // Configuration Repository
            services.AddScoped<IGenericRepository<OwnerEntity, OwnerContract, ManagePropertiesContext>, GenericRepository<OwnerEntity, OwnerContract, ManagePropertiesContext>>();
            services.AddScoped<IGenericRepository<PropertyEntity, PropertyContract, ManagePropertiesContext>, GenericRepository<PropertyEntity, PropertyContract, ManagePropertiesContext>>();
            services.AddScoped<IGenericRepository<PropertyImageEntity, PropertyImageContract, ManagePropertiesContext>, GenericRepository<PropertyImageEntity, PropertyImageContract, ManagePropertiesContext>>();
            services.AddScoped<IGenericRepository<PropertyTraceEntity, PropertyTraceContract, ManagePropertiesContext>, GenericRepository<PropertyTraceEntity, PropertyTraceContract, ManagePropertiesContext>>();

            // Add Interfaces
            services.AddScoped<IDBManagePropertiesRepository, DBManagePropertiesRepository>();
            services.AddTransient<IErrorLoggingClient, ErrorLoggingClient>();
            services.AddTransient<IOwnerService, OwnerService>();

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
