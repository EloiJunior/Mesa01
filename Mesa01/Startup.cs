using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;             //para definir a localização de formato de numeros e valores
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization; //para definir a localização de formato de numeros e valores
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;
using Mesa01.Services;

namespace Mesa01
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<Mesa01Context_context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Mesa01Context")));

            services.AddScoped<OperadorService>();      
            services.AddScoped<DepartamentoService>();  // registrar o DepartamentoService no sistema de injeção de dependencia
            services.AddScoped<FechamentoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //bloco para configurar a aplicação como sendo dos Estados Unidos
            var enUS = new CultureInfo("en-US");                         //criamos uma variavel para receber uma Informação de Cultura
            var localizationOptions = new RequestLocalizationOptions     //criação de objeto com as seguintes configurações:
            {
                DefaultRequestCulture = new RequestCulture(enUS),         //qual vai ser o local padrao da minha aplicação
                SupportedCultures = new List<CultureInfo> { enUS },       // quais são os locais possiveis da minha aplicação
                SupportedUICultures = new List<CultureInfo> { enUS }      // ??
            };

            app.UseRequestLocalization(localizationOptions);          //comando que vamos passar o nosso objeto de localização configurado acima


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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
