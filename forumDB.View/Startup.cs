using Microsoft.AspNetCore.Builder;
using forumDB.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using forumDB.View.Helper;

namespace forumDB.View
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSession();
            //services.AddMvc();
            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<ISessao, Sessao>();

            services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSession();
            //app.UseMvc();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
    //public class Startup
    //{
    //    public void Configure(IApplicationBuilder app)
    //    {
    //        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940



    //    }

    //    public void ConfigureServices(IServiceCollection services)
    //    {

    //        services.AddDistributedMemoryCache();

    //        services.AddSession(options =>
    //        {
    //            options.IdleTimeout = TimeSpan.FromSeconds(10);
    //            options.Cookie.HttpOnly = true;
    //            options.Cookie.IsEssential = true;
    //        });

    //        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    //        services.AddScoped<ISessao, Sessao>();

    //        services.AddSession(o =>
    //        {
    //            o.Cookie.HttpOnly = true;
    //            o.Cookie.IsEssential = true;
    //        });
    //    }
    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        app.UseSession();
    //    }

    //}
}
