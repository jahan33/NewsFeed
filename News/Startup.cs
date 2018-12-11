using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StructureMap;
using System;
using System.Text;

namespace News
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Register database service
            services.AddDbContext<NewsContext>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            // Add Authorization
            IConfigurationRoot config = new ConfigurationBuilder()
   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
   .AddJsonFile("appsettings.json")
   .Build();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = config.GetConnectionString("Issuer"),
                 ValidAudience = config.GetConnectionString("Issuer"),
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetConnectionString("Key")))
             };
         });

            // Add framework services.
            services.AddAutoMapper();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver
                    = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            })
                .AddControllersAsServices();

           

            string URLS = "";
            URLS = config["ConnectionStrings:URL"].Trim();
          

            string strOrg = config.GetConnectionString("URL");

            string[] origions = strOrg.Split(",");
            // services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(origions)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                    );
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));

            });
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                //  hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(1);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddLogging(builder => builder.AddLog4Net("log4net.config"));
            return ConfigureIoC(services);
        }
        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    scan.TheCallingAssembly();
                    scan.Assembly("DataAccess");
                 
               
                });           

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, NewsContext newsContext, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
          // Create database 
            newsContext.Database.EnsureCreated();

            IConfigurationRoot configuration = new ConfigurationBuilder()
   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
   .AddJsonFile("appsettings.json")
   .Build();
           
            loggerFactory.AddLog4Net("log4net.config");
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
         
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 180);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
