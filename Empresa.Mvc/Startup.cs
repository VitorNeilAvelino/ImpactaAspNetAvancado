using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Empresa.Mvc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            SetDataProtector();
        }

        private void SetDataProtector()
        {
            DataProtector = DataProtectionProvider.Create(this.GetType().FullName).CreateProtector(Configuration.GetSection("ChaveCriptografia").Value);
        }

        public IConfigurationRoot Configuration { get; }

        public IDataProtector DataProtector { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<EmpresaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EmpresaConnectionString")));

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IDataProtector>(DataProtector);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Master", policy =>
                    policy.RequireClaim("UserId", "1", "2", "3", "4", "5"));

                options.AddPolicy("EmissorNf", policy =>
                    policy.RequireRole("Contabil", "Administrativo")); // o usuário pode ter apenas um dos perfis.
            });

            // ASP.NET Core 2.0.
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // A ordem em que as configurações são feitas importa.
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = Configuration.GetSection("TipoAutenticacao").Value,
                LoginPath = new PathString("/Home/Login"),
                AccessDeniedPath = new PathString("/Home/AcessoNegado"),
                AutomaticAuthenticate = true, // Confirma se o usuário está autenticado em cada request. Caso contrário, apenas nos requests em que o [Authorize] esteja envolvido.
                AutomaticChallenge = true, // Sem o automático, o usuário não é reencaminhado automaticamente para a página de login.
                ExpireTimeSpan = TimeSpan.FromMinutes(10), // Default: 14 dias. Esse valor é um timeout de inatividade (dependendo da propriedade SlidingExpiration).

                // Default: true. Define que o ExpireTimeSpan serve para inatividade. Se estiver false, o cookie será expirado, não importa se o usuário interagiu ou não com a aplicação.
                //SlidingExpiration = false
            });


            // Tem que baixar os pacotes do Nuget.
            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?tabs=aspnetcore1x
            //app.Use(Facebook|Google|etc)Authentication(new FacebookOptions()
            //{
            //    AppId = Configuration["Authentication:Facebook:AppId"],
            //    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            //});

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}