using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using App.AttributeValidations;
using App.Data;
using AutoMapper;
using Business.Interfaces;
using Data.Context;
using Data.Repositories;
// using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.Configure<CookiePolicyOptions> (options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("DefaultConnection")));

            services.AddDbContext<MeuDbContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser> ()
                .AddDefaultUI (UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            services.AddAutoMapper (typeof (Startup));

            services.AddScoped<MeuDbContext> ();
            services.AddScoped<IProdutoRepository, ProdutoRepository> ();
            services.AddScoped<IEnderecoRepository, EnderecoRepository> ();
            services.AddScoped<IFornecedorRepository, FornecedorRepository> ();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAdapterProvider> ();

            services.AddMvc (o => {
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor (a => "Deve ser numero");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor (a => "Valor é invalido");
            }).SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCookiePolicy ();

            app.UseAuthentication ();

            var defaulCulture = new CultureInfo ("pt-BR");

            var cultureOptions = new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture (defaulCulture),
                SupportedCultures = new List<CultureInfo> { defaulCulture },
                SupportedUICultures = new List<CultureInfo> { defaulCulture }
            };

            app.UseRequestLocalization (cultureOptions);

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}