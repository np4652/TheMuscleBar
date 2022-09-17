using FluentMigrator.Runner;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Data;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Migrations;
using TheMuscleBar.AppCode.Reops;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Reflection;
using System.Collections.Generic;
using TheMuscleBar.AppCode.PaymentGateway;
using Microsoft.OpenApi.Models;
using System.IO;

namespace TheMuscleBar.AppCode.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("SqlConnection");
            GlobalDiagnosticsContext.Set("connectionString", dbConnectionString);
            IConnectionString ch = new ConnectionString { connectionString = dbConnectionString };

            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailService.EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddSingleton<IViewRenderService, ViewRenderService>();
            //services.AddScoped<IEmailService, EmailFactory>();
            services.AddSingleton<IConnectionString>(ch);
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserStore<ApplicationUser>, UserStore>();            
            services.AddScoped<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILog, LogNLog>();
            services.AddScoped<IRepository<EmailConfig>, EmailConfigRepo>();
     
            services.AddScoped<IReportService,Reops. ReportService>();
            //PackageService
            services.AddScoped<IPaymentGatewayService, PaymentGateway.PaymentGatewayService>();
            services.AddScoped<IPaymentGateway, Reops.PaymentGatewayService>();
           
     
            services.AddScoped<IAPILogin, APILogin>();
     
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<Database>();
            services.AddAutoMapper(typeof(Startup));
            services.AddHangfire(x => x.UseSqlServerStorage(dbConnectionString));
            services.AddHangfireServer();
            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2016()
                .WithGlobalConnectionString(configuration.GetConnectionString("SqlConnection"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
            List<NotificationPermissions> notificationPermissions = new List<NotificationPermissions>();
            configuration.GetSection("NotificationPermissions").Bind(notificationPermissions);
            services.AddSingleton(notificationPermissions);
            APIConfig apiConfig = new APIConfig();
            configuration.GetSection("APIConfig").Bind(apiConfig);
            services.AddSingleton(apiConfig);
            services.AddSingleton(apiConfig);
            JWTConfig jwtConfig = new JWTConfig();
            configuration.GetSection("JWT").Bind(jwtConfig);
            services.AddSingleton(jwtConfig);

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.1"
                });
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ApiDoc.xml");
                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "ApiDoc.xml");
                option.IncludeXmlComments(filePath);
                option.OperationFilter<AddRequiredHeaderParameter>();
                //var jwtSecurityScheme = new OpenApiSecurityScheme
                //{
                //    Scheme = "bearer",
                //    BearerFormat = "JWT",
                //    Name = "JWT Authentication",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                //    Reference = new OpenApiReference
                //    {
                //        Id = JwtBearerDefaults.AuthenticationScheme,
                //        Type = ReferenceType.SecurityScheme
                //    }
                //};
               // option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                //option.AddSecurityRequirement(
                //    new OpenApiSecurityRequirement{
                //        { jwtSecurityScheme, Array.Empty<string>() }
                //    });
                option.UseAllOfToExtendReferenceSchemas();
            });
        }
    }
}