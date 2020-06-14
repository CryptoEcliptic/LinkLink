using LinkLink.Data;
using LinkLink.Services;
using LinkLink.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LinkedBit
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Register 
            services.AddDbContextPool<ApplicationDbContext>(options =>
                                              options.UseSqlServer(_configuration.GetConnectionString("LinkLinkDbConnection")));

            //Register identity services
            services.AddIdentity<IdentityUser, IdentityRole>()
                                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings just for easy testing :)
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 2;
            });

            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<ICompanyServices, CompanyServices>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
