using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Project.AVIew.OtherAPI;
using Project.AVIew.EF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project.AVIew.Extensions;

namespace Project.AVIew
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
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.01", new OpenApiInfo { Title = "WEB", Version = "v1.01" });
            });

            services.AddOtherAPI();
            services.AddAVIewEFDbContext(Configuration);
            services.AddServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Account/SignInAccess";
                  options.AccessDeniedPath = "/Account/Denied";
              });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.01/swagger.json", "WEB v1.01");
                });
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("main",
                        pattern: "{title?}",
                        defaults: new
                        {
                            controller = "Home",
                            action = "index"
                        });

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action}/{id?}");

            });
        }
    }
}
