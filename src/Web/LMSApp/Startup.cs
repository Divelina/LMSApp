
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMSApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LMSApp.Models;
using LMSApp.Data.Models;
using LMSApp.Data.Common;
using LMSApp.Services.Mapping;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.DataServices;
using LMSApp.Services.Models.Courses;
using LMSApp.Services.Models.Users;
using LMSApp.Services.Models.Assignments;
using Microsoft.AspNetCore.Identity.UI.Services;
using LMSApp.Areas.Identity.Services;

namespace LMSApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CourseCreateBindingModel).Assembly,
                typeof(EducatorIdAndNameViewModel).Assembly,
                typeof(StudentAssignmentEditViewModel).Assembly
            );

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LMSAppContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<LMSAppUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                }
                )
                .AddEntityFrameworkStores<LMSAppContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddSession();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEducatorService, EducatorService>();
            services.AddScoped<ILectureciseService, LectureciseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWeekTimeService, WeekTimeService>();
            services.AddScoped<IEducatorLectureciseService, EducatorLectureciseService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IStudentAssignmentService, StudentAssignmentService>();
            services.AddScoped<IGroupService, GroupService>();

            //TODO - Register the custom services that work with the entities
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();
        }
    }
}
