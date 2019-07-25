using System;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authentication.Cookies;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;

using Agathas.Storefront.DependencyInjection;

namespace Agathas.Storefront.API {
  public class Startup {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to 
    // add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services) { 
      services.AddCors();
      services.AddResponseCompression();
      services.AddResponseCaching();
      services.AddHealthChecks();
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options => {
            options.LoginPath = "/api/account/logon";
            options.LogoutPath = "/api/account/signout";
          });
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  // needed for injecting dependency
     
      ConfigureAutoMapper(ref services);       
       services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 

      return ConfigureIoCContainer(ref services);
    }

    private void ConfigureAutoMapper(ref IServiceCollection services) {
      var mappingCfg = new MapperConfiguration( cfg => {
        cfg.AddProfile(new Services.AutoMapperBootStrapper());
      });

      var mapper = mappingCfg.CreateMapper();
      services.AddSingleton(mapper);
    }

    private AutofacServiceProvider ConfigureIoCContainer(ref IServiceCollection services){
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterModule<AutofacModule>();
      containerBuilder.Populate(services);
      
      var container = containerBuilder.Build();
      return new AutofacServiceProvider(container);
    }

    // This method gets called by the runtime. Use this method to configure 
    // the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
      else { app.UseHsts(); } // see https://aka.ms/aspnetcore-hsts.
      
      app.UseHttpsRedirection();
      app.UseResponseCompression();
      app.UseResponseCaching();
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
