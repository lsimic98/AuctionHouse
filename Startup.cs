using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Factories;
using AuctionHouse.Hubs;
using AuctionHouse.Models.Database;
using AuctionHouse.Models.Initializers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace AuctionHouse
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
            services.AddDbContext<AuctionHouseContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("AuctionHouseDB"))
            );

            services.AddIdentity<User, IdentityRole>(
                options => {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                }


            ).AddEntityFrameworkStores<AuctionHouseContext>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAutoMapper(typeof(Startup));

            services.AddQuartz(
                q =>
                {
                    q.SchedulerId = "Scheduler-Core";
                    //q.SchedulerName = "QuartzScheduler";
                    q.UseMicrosoftDependencyInjectionJobFactory(options =>{
                        // if we don't have the job in DI, allow fallback to configure via default constructor
                        options.AllowDefaultConstructor = true;
                    });
                    q.UseSimpleTypeLoader();
                    q.UseInMemoryStore();
                    q.UseDefaultThreadPool(tp =>
                    {
                        tp.MaxConcurrency = 10;
                    });

                    
                }
            );


           
        

            services.ConfigureApplicationCookie(
                options => {
                    options.LoginPath = "/User/LogIn";
                    options.AccessDeniedPath = "/Home/Error";
                    
                }
            );
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimFactory>();

            services.AddSignalR();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                UserInitializer.initialize(userManager);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapHub<AuctionHub>("/update");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auction}/{action=Index}/{id?}");
            });
        }
    }
}
