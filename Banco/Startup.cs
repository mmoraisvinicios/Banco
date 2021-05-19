using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Banco.Models;
using Banco.Validacao;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Domain.Servicos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infra.Context;
using Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CrossCutting;

namespace Banco
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
            services.AddCors();
            services.AddSpaStaticFiles(dir =>
            {
                dir.RootPath = "banco-UI";
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //
            services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ContextContaBancaria")));

            services.AddControllers()
               .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddNewtonsoftJson(op =>
            {
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<IBaseRepositorio<Conta>, BaseRepository<Conta>>();
            services.AddScoped<IBaseServico<Conta>, BaseService<Conta>>();

            services.AddHttpClient<IBancos, APIListaBancosClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["APIListaBancos:UrlBase"]);
            });

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<Conta, ListarContas>()
                    .ForMember(dest => dest.ContaId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao ? "Ativo" : "Inativo"))
                    .ReverseMap();

                config.CreateMap<AtualizarConta, Conta>()
                  .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => true))
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContaId)).ReverseMap();

                config.CreateMap<NovaConta, Conta>()
                .ForMember(dest => dest.DataAbertura, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => true));
            }).CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "banco-UI");

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            });
        }
    }
}
