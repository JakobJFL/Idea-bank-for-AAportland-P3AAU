using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataBaseLib.DataAccess;
using BusinessLogicLib.Interfaces;
using RepositoryLib.Interfaces;
using RepositoryLib.Implementations;
using BusinessLogicLib.Service;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Ideabank.Areas.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IdeaBank
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<IIdeaRepository, IdeaRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();
            services.AddTransient<IBusinessUnitsRepository, BusinessUnitsRepository>();
            
            services.AddTransient<IConfig, Config>();
            services.AddTransient<IIdeasDataAccess, IdeasDataAccess>();    
            services.AddTransient<ICommentsDataAccess, CommentsDataAccess>();
            services.AddTransient<IBusinessUnitsDataAccess, BusinessUnitsDataAccess>();
            services.AddTransient<IDepartmentsDataAccess, DepartmentsDataAccess>();
            services.AddTransient<ICsvService, CsvService>();
            services.AddSingleton<Settings>();

            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            }, ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<Context>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
