using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patent.Areas.Identity;
using Patent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Patent.Services;
using Sender;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utiles;
using BoardProject.Repository;
using BoardProject;

namespace Patent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PostDbContext>(Option => Option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            // services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<IEmailSender>(e => new EmailSender("" +
                "kangbk41882@gmail.com", 
                "dfwrqihlnbmpygwk"
                )
            );

            services.AddSingleton<IMessageSender>(e => new MessageSender(
                     "01046784188",
                     new MessageCore("NCSBAXMKUG71F1PS","JAC0CLTVK4THC14YJXYAVTGDN4HVYT5K")
                )
            );

            services.AddScoped<IPostRepository, PostRepository>();
            //Board에서 쓰임 
            services.AddScoped<BrowserService>();
            services.AddSingleton<IVerifyPhoneNumber, VerifyPhoneNumber>();

            services.AddSingleton<ISaveAndLoad, SaveAndLoad>();

            services.Configure<SMSoptions>(Configuration);
            services
            .AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true; // optional
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
