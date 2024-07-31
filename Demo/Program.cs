using Demo.BL.Interface;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Extend;
using Demo.Language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddNewtonsoftJson(opt => {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });


            //to add the connectio string injection to the dbcontext 

            builder.Services.AddDbContext<DemoContext>(optionBuilder =>
            optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Constr")));

            //to make the Auto Mapper

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            //Transiant (one instance for every operations)

            // builder.Services.AddTransient<IDepartmentRepo, DepartmentRepo>();


            //Scoped  (one instance for each user for all operations)

            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<ICountryRepo, CountryRepo>();
            builder.Services.AddScoped<ICityRepo, CityRepo>();
            builder.Services.AddScoped<IDistrictRepo, DistrictRepo>();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });



            builder.Services .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Default Password settings.
                //options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<DemoContext>()
           .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


            //SingleTone (one instance for all users)

            // builder.Services.AddSingleton<IDepartmentRepo, DepartmentRepo>();
           


            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                      };
               // All Language in Website
            app.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture("en-US"),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures,
                    RequestCultureProviders = new List<IRequestCultureProvider>
                {
             new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
                });



             
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                 name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            app.Run();
        }
    }
}
