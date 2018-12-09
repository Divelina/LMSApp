using CommandLine;
using LMSApp.Data;
using LMSApp.Data.Common;
using LMSApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            // Seed data on application startup
            //using (var serviceScope = serviceProvider.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    dbContext.Database.Migrate();
            //    ApplicationDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            //}

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                SandboxCode(serviceProvider);

                //return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                //    (SandboxOptions opts) => SandboxCode(opts, serviceProvider),
                //    _ => 255);
            }
        }


        //private static int SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        private static int SandboxCode(IServiceProvider serviceProvider)
        {
            //TODO write code here;

            var context = serviceProvider.GetService<LMSAppContext>();
            var rolemanager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var usermanager = serviceProvider.GetService<UserManager<LMSAppUser>>();

            var roleSeeder = new RoleSeeder(context, usermanager, rolemanager);

            roleSeeder.CreateRolesAsync().Wait();

            //var sw = Stopwatch.StartNew();
            //var settingsService = serviceProvider.GetService<ISettingsService>();
            //Console.WriteLine($"Count of settings: {settingsService.GetCount()}");
            //Console.WriteLine(sw.Elapsed);

            return 0;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<LMSAppContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<LMSAppUser, IdentityRole>(
                  options =>
                  {
                      options.Password.RequiredLength = 6;
                      options.Password.RequireDigit = false;
                      options.Password.RequireNonAlphanumeric = false;
                      options.Password.RequireUppercase = false;
                      options.Password.RequireLowercase = false;
                      options.User.RequireUniqueEmail = true;
                  }
                  )
                  .AddEntityFrameworkStores<LMSAppContext>()
                  .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));

            //services.AddSingleton<IConfiguration>(configuration);

            //services.AddDbContext<LMSAppContext>(
            //    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            //        .UseLoggerFactory(new LoggerFactory()));

            //services
            //    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            //    {
            //        options.Password.RequireDigit = false;
            //        options.Password.RequireLowercase = false;
            //        options.Password.RequireUppercase = false;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.Password.RequiredLength = 6;
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddUserStore<ApplicationUserStore>()
            //    .AddRoleStore<ApplicationRoleStore>()
            //    .AddDefaultTokenProviders();

            //services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            //// Application services
            //services.AddTransient<IEmailSender, NullMessageSender>();
            //services.AddTransient<ISmsSender, NullMessageSender>();
            //services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}
