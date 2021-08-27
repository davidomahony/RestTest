using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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

            services.AddHttpClient<IInformationFetcher<PokemonSpeciesModel>, PokemonSpeciesFetcher>(cl =>
                cl.BaseAddress = new Uri(@"https://pokeapi.co/api/v2/pokemon-species/"));
            services.AddHttpClient<IStringTranslater, YodaTranslater>(cl =>
                cl.BaseAddress = new Uri(@"https://api.funtranslations.com/translate/shakespeare.json"));
            services.AddHttpClient<IStringTranslater, ShakespeareTranslater>(cl =>
                cl.BaseAddress = new Uri(@"https://api.funtranslations.com/translate/yoda.json"));
        
            services.AddSingleton<PokemonUtility>();
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
