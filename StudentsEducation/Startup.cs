using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Services;
using StudentsEducation.Infrastructure.Data;
using StudentsEducation.Infrastructure.Repository;

namespace StudentsEducation
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
            //also some services are in the identity/IdentityHostingStartup

            //db
            services.AddDbContext<EducationDbContext>(options => 
            options.UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); ;

           // services.AddRazorPages();
            //mvc
            services.AddControllersWithViews();
            services.AddTransient<StudentsService>();
            //repos injection
            services.AddScoped(typeof(IAsyncRepository<>),typeof(EFRepository<>));
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            //identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
