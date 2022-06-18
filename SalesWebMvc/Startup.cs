using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Data;
using SalesWebMvc.Services;


namespace SalesWebMvc
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

            services.AddDbContext<SalesWebMvcContext>(options => options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder =>
            builder.MigrationsAssembly("SalesWebMvc")));

            services.AddScoped<SeedingService>();//Registra o servico de injeção de dependencias da aplicação em outras classes
            services.AddScoped<SellerService>();//serviço pode ser injetado em outras classes
            services.AddScoped<DepartmentService>();//Gera um lista apartir do banco com os departamentos ordenados.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())//Testa se esta no perfil de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else
            {     //verifica se o aplicativo ja esta publicado e executa as config abaixo...
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
                    template: "{controller=Home}/{action=Index}/{id?}");//aqui toda vez que o site iniciar como controller padrao sobe a page home, se nenhuma acao for feita sobe index e o id e opcional
            });
        }

    }
}
