using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Data;
using Test_Data.Repository;
using Test_Library.Configs;
using Test_Library.Helper;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Test_Core.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Test_Web.Hubs;
using Test_Web.Controllers;

namespace Test_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Net core 2.2
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()
                .AddJwtBearer(ApiConfig.Auth_Bearer_Mobile, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer_Mobile"],
                        ValidAudience = Configuration["Jwt:Issuer_Mobile"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityModel.Mobile_Key))
                    };
                });
            //kêt nối db
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:DefaultConnection").Result;
            services.AddDbContext<Kztek_Entities>(opts => opts.UseSqlServer(connect));

            services.AddCors(options =>
  options.AddPolicy("Dev", builder =>
  {
      // Allow multiple methods  
      builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
        .WithHeaders(
          HeaderNames.Accept,
          HeaderNames.ContentType,
          HeaderNames.Authorization)
        .AllowCredentials()
        .SetIsOriginAllowed(origin =>
        {
            if (string.IsNullOrWhiteSpace(origin)) return false;
          // Only add this to allow testing with localhost, remove this line in production!  
          if (origin.ToLower().StartsWith("http://localhost")) return true;
          // Insert your production domain here.  
          if (origin.ToLower().StartsWith("https://dev.mydomain.com")) return true;
            return false;
        });
  })
);
            services.AddRazorPages();
            // Đăng ký ChatController và ChatHub
            //services.AddScoped<ChatController>();
            services.AddScoped<ChatHub>();
            services.AddSession();
            services.AddSignalR(); // Thêm cấu hình SignalR
            services.AddMemoryCache();
            services.AddSingleton<CacheHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddCors();
            //Cấu hình bảo mật API
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(ApiConfig.Auth_Bearer_Mobile)
                .Build();

                options.AddPolicy(ApiConfig.Auth_Bearer_Mobile, policy =>
                {
                    policy.AuthenticationSchemes.Add(ApiConfig.Auth_Bearer_Mobile);
                    policy.RequireAuthenticatedUser();
                });
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
  {
      options.Cookie.Name = "UserLoginCookie";
      options.SlidingExpiration = true;
      options.ExpireTimeSpan = new TimeSpan(1, 0, 0); // Expires in 1 hour  
      options.Events.OnRedirectToLogin = (context) =>
      {
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
      };

      options.Cookie.HttpOnly = true;
      // Only use this when the sites are on different domains  
      options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
  });
            //Autofac
            //services.AddScoped<IDependencyOne, DependencyOne>();
            //services.AddScoped<IDependencyTwoThatIsDependentOnDependencyOne, DependencyTwoThatIsDependentOnDependencyOne>();
            var builder = new ContainerBuilder();
           
            builder.RegisterAssemblyTypes(typeof(MemberRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            // back end

            builder.RegisterAssemblyTypes(typeof(Test_Service.Admin.Database.SQLSERVER.MemberService).Assembly)
                .Where(t => t.Name.EndsWith("Service") && t.Namespace.Contains("Admin"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //
            builder.Populate(services);
            var container = builder.Build();
            //Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var MyAllowSpecificOrigins = "*";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseCors(options =>
    options.WithOrigins("http://localhost:3000")
           .AllowAnyHeader()
           .AllowAnyMethod());
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(
       new CookiePolicyOptions
       {
           Secure = CookieSecurePolicy.Always
       });
            //app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //name: "",
                //pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub"); // Map đường dẫn cho ChatHub
            });
        }
    }
}
