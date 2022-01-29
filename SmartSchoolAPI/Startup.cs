using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartSchoolAPI.Data;

namespace SmartSchoolAPI
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
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddVersionedApiExplorer(opt => 
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1,0);
                opt.ReportApiVersions = true; 
            });


            var apiProviderDescription = services.BuildServiceProvider()
                                                .GetService<IApiVersionDescriptionProvider>();   

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new 
                    OpenApiInfo { 
                        Title = "SmartSchool API", 
                        Version = description.ApiVersion.ToString(),
                        TermsOfService = new Uri("https://github.com/posccis/SmartSchool-.Net-MVC") ,
                        Description = "API desenvolvida para a prática no curso de WEB API com .Net da Udemy.",
                        License = new OpenApiLicense {
                            Name = "SmartSchool License",
                            Url = new Uri("http://mit.com")
                        },
                        Contact = new OpenApiContact {
                            Name = "Victor Hugo",
                            Email = "victrgmes@gmail.com"
                        }

                        });
                }

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                c.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddScoped<IRepository, Repository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IWebHostEnvironment env,
                                IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>{ 
                    
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    
                                        c.RoutePrefix = "";
                });
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
