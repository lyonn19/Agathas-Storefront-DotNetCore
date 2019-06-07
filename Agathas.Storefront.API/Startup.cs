using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using System;
using System.Linq;

namespace Agathas.Storefront.API {
  public class Startup {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services) { 
      services.AddCors();
      services.AddResponseCompression();
      services.AddResponseCaching();
      services.AddHealthChecks();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 

      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterModule<AutofacModule>();
      containerBuilder.Populate(services);
      
      var container = containerBuilder.Build();
      return new AutofacServiceProvider(container);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
      else {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      
      app.UseHttpsRedirection();
      app.UseResponseCompression();
      app.UseResponseCaching();
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
