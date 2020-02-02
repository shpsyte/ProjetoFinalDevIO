using System.Collections.Generic;
using System.Globalization;
using App.AttributeValidations;
using App.Data;
using AutoMapper;
using Business.Interfaces;
using Data.Context;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Config {
    public static class StartupConfig {

        public static IServiceCollection AdicionarMinhaInjecao (this IServiceCollection services) {

            services.AddAutoMapper (typeof (Startup));

            services.AddScoped<MeuDbContext> ();
            services.AddScoped<IProdutoRepository, ProdutoRepository> ();
            services.AddScoped<IEnderecoRepository, EnderecoRepository> ();
            services.AddScoped<IFornecedorRepository, FornecedorRepository> ();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAdapterProvider> ();

            return services;
        }

        public static IServiceCollection AddMeuMvc (this IServiceCollection services) {

            services.AddMvc (o => {
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor (a => "Deve ser numero");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor (a => "Valor é invalido");
            }).SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection AddIdentityConfig (this IServiceCollection services, IConfiguration configuration) {

            services.Configure<CookiePolicyOptions> (options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (
                    configuration.GetConnectionString ("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser> ()
                .AddDefaultUI (UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            return services;
        }

        public static IServiceCollection AddContext (this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<MeuDbContext> (options =>
                options.UseSqlServer (
                    configuration.GetConnectionString ("DefaultConnection")));

            return services;
        }

        public static IApplicationBuilder CultureCOnfig (this IApplicationBuilder app) {
            var defaulCulture = new CultureInfo ("pt-BR");

            var cultureOptions = new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture (defaulCulture),
                SupportedCultures = new List<CultureInfo> { defaulCulture },
                SupportedUICultures = new List<CultureInfo> { defaulCulture }
            };

            app.UseRequestLocalization (cultureOptions);

            return app;
        }

    }
}