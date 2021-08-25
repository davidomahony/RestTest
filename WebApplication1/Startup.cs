using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;
using WebApplication1.Translater;
using WebApplication1.Utilities;

namespace WebApplication1
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
            services.AddControllers();

            services.AddHttpClient<IInformationFetcher<PokemonSpeciesModel>, PokemonSpeciesInformaitonFetcherr>();
            services.AddHttpClient<IStringTranslater, YodaTranslater>();
            services.AddHttpClient<IStringTranslater, ShakespeareTranslater>();

            //services.AddSingleton<IStringTranslater, YodaTranslater>();
            //services.AddSingleton<IStringTranslater, ShakespeareTranslater>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
