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
using FooseStats.Data.FooseStats.Data.Ef;

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
                cnfg.CreateMap<TournamentHeader, TournamentDto>();
                cnfg.CreateMap<TournamentCreationDto, TournamentHeader>();

                //Entity to entity for copying maps
                CreateUpdateableMap<Player>(cnfg);
                CreateUpdateableMap<Match>(cnfg);
                CreateUpdateableMap<MatchType>(cnfg);
                CreateUpdateableMap<AlmaMater>(cnfg);
                CreateUpdateableMap<Location>(cnfg);
                CreateUpdateableMap<TournamentHeader>(cnfg);
                CreateUpdateableMap<TournamentRelation>(cnfg);
            });

            services.AddSingleton<IBaseDA<Player>>(new BaseDAService<Player>((FooseStatsContext db) => db.Players, (P1, P2) => P1.PlayerId.Equals(P2.PlayerId)));
            services.AddSingleton<IBaseDA<Match>>(new BaseDAService<Match>((FooseStatsContext db) => db.Matches, (M1, M2) => M1.MatchId.Equals(M2.MatchId)));
            services.AddSingleton<IBaseDA<MatchType>>(new BaseDAService<MatchType>((FooseStatsContext db) => db.MatchTypes, (MT1, MT2) => MT1.MatchTypeId.Equals(MT2.MatchTypeId)));
            services.AddSingleton<IBaseDA<Location>>(new BaseDAService<Location>((FooseStatsContext db) => db.Locations, (L1, L2) => L1.LocationId.Equals(L2.LocationId)));
            services.AddSingleton<IBaseDA<AlmaMater>>(new BaseDAService<AlmaMater>((FooseStatsContext db) => db.AlmaMaters, (AM1, AM2) => AM1.AlmaMaterId.Equals(AM2.AlmaMaterId)));
            services.AddSingleton<IBaseDA<TournamentHeader>>(new BaseDAService<TournamentHeader>((FooseStatsContext db) => db.TournamentHeaders, (TH1, TH2) => TH1.TournamentId.Equals(TH2.TournamentId)));
            services.AddSingleton<IBaseDA<TournamentRelation>>(new BaseDAService<TournamentRelation>((FooseStatsContext db) => db.TournamentRelations, (TR1, TR2) => TR1.TournamentRelationId.Equals(TR2.TournamentRelationId)));
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
