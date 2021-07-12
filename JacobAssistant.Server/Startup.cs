using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Bots.TelegramBots;
using JacobAssistant.Common.Models;
using JacobAssistant.Email;
using JacobAssistant.ScheduleTask;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace JacobAssistant
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "JacobAssistant.Server", Version = "v1"});
            });
            services.AddDbContext<AssistantDbContext>();
            services.AddScoped<ConfigService, ConfigService>(provider =>
                new ConfigService(
                    provider.CreateScope().ServiceProvider.GetService<AssistantDbContext>(),
                    provider.GetService<IHostEnvironment>().IsDevelopment()));
            services.AddSingleton<SimpleCommands>();

            services.AddTransient<BotOptions, BotOptions>(pro =>
                pro.CreateScope().ServiceProvider.GetService<ConfigService>()?.BotOptions());
            services.AddSingleton<AssistantBotClient, AssistantBotClient>();

            services.AddScoped<EmailAccountService>();
            services.AddSingleton(provider => new EmailHandler(provider.GetService<AssistantBotClient>()
                , provider.CreateScope().ServiceProvider.GetService<EmailAccountService>()));
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<EmailJob>();
            services.AddSingleton(new JobSchedule(typeof(EmailJob), "0 0/5 * * * ? *"));
            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AssistantBotClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JacobAssistant.Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            // 启动Bot
            client.Start();
        }
    }
}