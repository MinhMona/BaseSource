﻿using Application.Services;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace BaseAPI
{
    /// <summary>
    /// Configuration of extension services
    /// </summary>
    public static class ConfigureExtension
    {
        /// <summary>
        /// ConfigureService
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<ITokenManagerService, TokenManagerService>();
        }

        /// <summary>
        /// ConfigureSwagger
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseSource", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt auth header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Configure Repository
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
