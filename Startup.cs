using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using WebLanchesMVC.Areas.Admin.Services;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;
using WebLanchesMVC.Repositories;

namespace WebLanchesMVC
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
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Home/AccessDenied");

			services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<ILunchRepository, LunchRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<ReportSalesService>();

			services.AddScoped(cp => CartPurchase.GetCart(cp));
            services.AddControllersWithViews();

			services.AddPaging(options => {
				options.ViewName = "Bootstrap4";
				options.PageParameterName = "pageindex";
			});

			services.AddMemoryCache();
			services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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
			app.UseSession();

			app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
				endpoints.MapControllerRoute(
                    name: "AdminArea",
					pattern: "{area:exists}/{Controller=Admin}/{Action=Index}{id?}");

				endpoints.MapControllerRoute(
                    name: "categoryFilter",
					pattern: "Lunch/{action}/{category?}",
                    defaults: new { Controller = "Lunch", action = "List" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
