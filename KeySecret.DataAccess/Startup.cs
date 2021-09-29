using KeySecret.DataAccess.Data;
using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Accounts.Repositories;
using KeySecret.DataAccess.Library.Categories.Models;
using KeySecret.DataAccess.Library.Categories.Repositories;
using KeySecret.DataAccess.Library.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KeySecret.DataAccess
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionString = Configuration.GetConnectionString("KeySecretData");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

            services.AddSingleton<IRepository<AccountModel>, AccountsRepository>(repository => new AccountsRepository(ConnectionString))
                    .AddSingleton<IRepository<CategoryModel>, CategoryRepository>(repository => new CategoryRepository(ConnectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}