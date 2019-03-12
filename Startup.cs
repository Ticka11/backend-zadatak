using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd_zadatak.Data;
using BackEnd_zadatak.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace BackEnd_zadatak
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
            //konekcija sa sql serverom
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Default")));

            //dodjamo JsonOptions da ne bi dobijali self referencing gresku u konzoli
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                            .AddJsonOptions(opt => {
                                            opt.SerializerSettings.ReferenceLoopHandling = 
                                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                                        });;

            //registrovanje swagger generatora, i definisanje swagger dokumenta
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v4", new Info { Title = "Devices API", Version = "v4" });

                 //locira xml file generisan od strane asp.net core-a
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                //govori swaggeru da koristi xml komentare
                c.IncludeXmlComments(xmlPath);

            });
            services.AddCors();

            services.AddAutoMapper();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v4/swagger.json", "Devices API");
            });
        }
    }
}
