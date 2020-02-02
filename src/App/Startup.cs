using App.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App {
    public class Startup {

        public Startup (IHostingEnvironment host) {

            var builder = new ConfigurationBuilder ()

                .SetBasePath (host.ContentRootPath)
                .AddJsonFile ("appsettings.json", true, true)
                .AddJsonFile ($"appsettings.{host.EnvironmentName}.json", true, true);

            Configuration = builder.Build ();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {

            services.AddIdentityConfig (Configuration);
            services.AddContext (Configuration);
            services.AdicionarMinhaInjecao ();
            services.AddMeuMvc ();

        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCookiePolicy ();
            app.UseAuthentication ();
            app.CultureCOnfig ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}