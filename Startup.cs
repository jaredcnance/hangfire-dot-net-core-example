using System;
using concurrent_queues.Data;
using concurrent_queues.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace concurrent_queues
{
  public class Startup
  {
    public IConfigurationRoot Configuration { get; }

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration["Data:DefaultConnection"]),
              ServiceLifetime.Transient);

      services.AddHangfire(config =>
      {
        config.UseSqlServerStorage(Configuration["Data:WorkQueue"]);
      });

      services.AddSingleton(new TodoItemService(Configuration));

      services.AddMvc();
    }
    
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      GlobalConfiguration.Configuration.UseActivator(new ServiceProviderActivator(serviceProvider));
      
      app.UseHangfireServer();

      app.UseMvc();
    }
  }
}
