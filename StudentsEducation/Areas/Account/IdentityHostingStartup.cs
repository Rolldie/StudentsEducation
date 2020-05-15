using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentsEducation.Infrastructure.Identity;
using StudentsEducation.Infrastructure.Identity.Data;

[assembly: HostingStartup(typeof(StudentsEducation.Web.Areas.Account.IdentityHostingStartup))]
namespace StudentsEducation.Web.Areas.Account
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AccountDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AccountConnection")));
                services.AddAuthorization(options=> {
                    options.AddPolicy("IsAdmin", policy => policy.RequireRole("Administrator"));
                    options.AddPolicy("IsTeacher", policy => policy.RequireRole("Teacher"));
                });
                services.AddIdentity<AppUser,Role>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AccountDbContext>().AddRoles<Role>();

                services.AddHttpContextAccessor();
                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                });
            });
        }
    }
}