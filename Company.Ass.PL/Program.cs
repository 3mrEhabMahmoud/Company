using Company.Ass.BLL.Interfaces;
using Company.Ass.BLL.Repositories;
using Company.Ass.DAL.Data.Contexts;
using Company.Ass.PL.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Company.Ass.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();//Allow DI FOR DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//Allow DI FOR EmployeeRepository

            builder.Services.AddDbContext<CompanyDbContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
            });//Allow DI FOR CompanyDbContext


            //Life Time
            //builder.Services.AddScoped(); //Create object Life Time Per Request - UnReachable Object
            //builder.Services.AddTransient(); //Create object Life Time Per Operation
            //builder.Services.AddSingleton(); //Create object Life Timer Per App

            builder.Services.AddScoped<IScopedService, ScopedService>();//Per Request
            builder.Services.AddTransient<ITransentService,TransentService>();//Per Operation
            builder.Services.AddSingleton<ISingletonService, SingletonService>();//per App



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
