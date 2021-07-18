using JacobAssistant.Bots.Commands;
using JacobAssistant.Bots.TelegramBots;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Models;
using JacobAssistant.Email;
using JacobAssistant.Extension;
using JacobAssistant.ScheduleTask;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Utilities;
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

            services.AddDbContext<ConfigurationDbContext>(options=> 
                options.UseMySQL(Configuration["ConfigurationSource:Mysql"]) );

            /*
            services.AddTransient<BotOptions, BotOptions>(pro =>
                pro.CreateScope().ServiceProvider.GetService<ConfigService>()?.BotOptions());*/
            /*services.AddSingleton<AssistantBotClient>(new BotOptions(Configuration[ConfigMapping.TelegramDevBotToken],
                    Configuration[ConfigMapping.TelegramAdminId],
                    Configuration[ConfigMapping.TelegramAnnounceChannelId]),
                Configuration);*/
            services.AddSingleton<BotOptions>(provider => new BotOptions(Configuration[ConfigMapping.TelegramDevBotToken],
                    long.Parse(Configuration[ConfigMapping.TelegramAdminId]),
                long.Parse(Configuration[ConfigMapping.TelegramAnnounceChannelId])));
            services.AddSingleton<AssistantBotClient,AssistantBotClient>();
            
            
            services.AddScoped<EmailAccountService>();
            services.AddSingleton(provider => new EmailHandler(provider.GetService<AssistantBotClient>()
                , provider.CreateScope().ServiceProvider.GetService<EmailAccountService>()));
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddTransient<EmailJob>();
            // services.AddSingleton(new JobSchedule(typeof(EmailJob), "0 0/5 * * * ? *"));
            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,AssistantBotClient client)
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
            // app.UseBots();
        }
    }
}