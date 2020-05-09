using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsEducation.Domain.Services;
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
                       .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRazorPages();
            //mvc
            services.AddControllersWithViews();

            services.AddServerSideBlazor();
            //services
            services.AddScoped<IStudentsService,StudentsService>();
            services.AddScoped<ICathedrasAndGroupsService,CathedraManageService>();
            services.AddScoped<IdentityService>(); 
            //repos injection
            services.AddScoped(typeof(IAsyncRepository<>),typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
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
                app.UseExceptionHandler("~/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();

            //identity
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
            });
        }
    }
}
