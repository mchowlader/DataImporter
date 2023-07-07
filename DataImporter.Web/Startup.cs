using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataImporter.Common;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileCreate;
using DataImporter.Transfer;
using DataImporter.Transfer.Contexts;
using DataImporter.Transfer.Entities;
using DataImporter.User;
using DataImporter.User.Contexts;
using DataImporter.User.Entities;
using DataImporter.User.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            WebHostEnvironment = env;

            Configuration = builder.Build();
        }
        
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();

            builder.RegisterModule(new TransferModule(connectionInfo.connectionString, 
                connectionInfo.migrationAssemblyName));

            builder.RegisterModule(new UserModule(connectionInfo.connectionString, 
                connectionInfo.migrationAssemblyName));
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new WebModule());
            builder.RegisterModule(new CreateExcelModule());
        }

        private (string connectionString, string migrationAssemblyName) GetConnectionStringAndAssemblyName()
        {
            var connectionString = "DefaultConnection";
            var connectionStringName = Configuration.GetConnectionString(connectionString);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            return (connectionStringName, migrationAssemblyName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();

            services.AddDbContext<ApplicationDbContext>(option =>
               option.UseSqlServer(connectionInfo.connectionString,
                   x => x.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDbContext<TransferDbContext>(option =>
                option.UseSqlServer(connectionInfo.connectionString,
                    x => x.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            // Identity customization started here
            services
                .AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.Cookie.Name = "CustomerPortal.Identity";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //reCapchaa
            services.AddHttpClient<ReCaptcha>(x =>
            {
                x.BaseAddress = new Uri("https://www.google.com/recaptcha/api/siteverify");
            });
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{Id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");

                endpoints.MapRazorPages();

            });

        }
    }
}
