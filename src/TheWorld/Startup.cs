﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using TheWorld.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using TheWorld.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authentication.Cookies;
using System.Net;
using AspNetIdentity.Services;

namespace TheWorld
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(appEnv.ApplicationBasePath)
              .AddJsonFile("config.json")
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
#if !DEBUG
                config.Filters.Add(new RequireHttpsAttribute());
#endif
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonLetterOrDigit = false;
                config.Password.RequireDigit = false;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if(ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode==(int)HttpStatusCode.OK)
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        else
                            ctx.Response.Redirect(ctx.RedirectUri);
                        return Task.FromResult(0);
                    }
                };
            })
            .AddEntityFrameworkStores<WebPostContext>();

            services.AddLogging();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<WebPostContext>();
            services.AddTransient<WebPostContextSeedData>();
            services.AddTransient<UserProfile>();
            services.AddTransient<WebPostBR>();
            services.AddScoped<IWebPostRepository, WebPostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, WebPostContextSeedData seeder, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddDebug(LogLevel.Warning);
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIdentity();
            Mapper.Initialize(config =>
            {
                config.CreateMap<WebPost, WebPostViewModel>().ReverseMap();

               // config.CreateMap<string, PostText>()
               // .ForMember(dest => dest.PostString, opt => opt.MapFrom(so => so));

               // config.CreateMap<IFormFile, PostFile>()
               //.ForMember(dest => dest.PostBytes, opt => opt.MapFrom(so => Config.getIFormFileBytes(so)));

            });
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "WebPosts", id="1" }
                    );
            });
            await seeder.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
