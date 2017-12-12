using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FooseStats.Data.Dto;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using FooseStats.Data.Interfaces;
using FooseStats.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FooseStats.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.PrivateApiKeys.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddCors();

            Mapper.Initialize(cnfg =>
            {
                //Entity to Dto Maps
                cnfg.CreateMap<Player, PlayerDto>();
                cnfg.CreateMap<Player, RivalDto>();

                //Entity to entity for copying maps
                CreateUpdateableMap<Player>(cnfg);
                CreateUpdateableMap<Match>(cnfg);
                CreateUpdateableMap<MatchType>(cnfg);
                CreateUpdateableMap<AlmaMater>(cnfg);
                CreateUpdateableMap<Location>(cnfg);
            });

            services.AddSingleton<IPlayerDA>(new FoosePlayerDAService());
            services.AddSingleton<IMatchDA>(new FooseMatchDAService());
            services.AddSingleton<IMatchTypeDA>(new FooseMatchTypeDAService());
            services.AddSingleton<IConfigurationRoot>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => {
                builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            app.UseMvc();
        }

        private void CreateUpdateableMap<T>(IMapperConfigurationExpression cnfg) where T : IUpdatable
        {
            cnfg.CreateMap<T, T>()
                    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdateDate, opt => opt.ResolveUsing<DateTime>(x => DateTime.Now));
        }
    }
}
