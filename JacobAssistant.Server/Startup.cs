using System;
using JacobAssistant.Bots.Extensions;
using JacobAssistant.Bots.TelegramBots;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Models;
using JacobAssistant.Extension;
using JacobAssistant.Schedule.Extensions;
using JacobAssistant.Schedule.Jobs;
using JacobAssistant.Services;
using JacobAssistant.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;

namespace JacobAssistant
{
    public class Startup
    {
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "JacobAssistant.Server", Version = "v1"});
            });

            services.AddDbContext<ConfigurationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("Mysql")));

            services.Configure<AppOptions>(Configuration.GetSection(AppOptions.App));
            // 环境区分
            if (!_env.IsEnvironment("Development2"))
            {
                Log.Information("trusted env , start AddBot");
                services.AddBots(Configuration, _env);
                //todo:待测试
                services.AddSingleton<IAnnounceService, AssistantBotClient>();
            }

            services.AddSingleton<IAnnounceService,ConsoleAnnounceService>();
            //定时任务
            services.AddEmailRemindJob();
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JacobAssistant.Server v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}