using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Demo.BL.Interface;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Microsoft.EntityFrameworkCore;

namespace Demo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<DemoContext>(optionBuilder =>
                optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Constr")));

            // Add AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new DomainProfile());
            });

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Add Scoped Services
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthorization();

             

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
