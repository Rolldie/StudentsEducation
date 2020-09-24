using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsEducation.Domain.Services;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Web.Areas.Account.Services;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EducationDbContext>(options => 
                options.UseLazyLoadingProxies().EnableSensitiveDataLogging()
                       .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRazorPages().AddRazorPagesOptions(options=>
            {
                options.Conventions.AuthorizeAreaFolder("Admin", "/","IsAdmin");
                options.Conventions.AuthorizeAreaFolder("TeachersPanel", "/", "IsTeacher");
            }
            );
         
            services.AddServerSideBlazor();
            
            //services
            services.AddScoped<IStudentsService,StudentsService>();
            services.AddScoped<ICathedrasAndGroupsService,CathedraManageService>();
            services.AddScoped<IdentityService>();
            services.AddScoped<ITeachersAndScheduleSerivce, TeacherManageService>();
            services.AddScoped<ISubjectAndWorksService, SubjectManageService>();
            services.AddScoped(typeof(IAsyncRepository<>),typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithRedirects("~/Error?code{0}");
            app.UseExceptionHandler("/Error");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
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
