using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using BLL.App;
using CarApp.BLL.App;
using CarApp.DAL.App;
using DAL.App.DTO.MappingProfiles;
using DAL.App.EF;
using DAL.App.EF.DataInit;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication.Areas.Identity.IdentityErrorDescriber;
using WebApplication.Helpers;

namespace WebApplication
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration">App configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            //CustomIdentityErrorDescriber uses localization
            services.AddLocalization(options => options.ResourcesPath = "Resource.Base");

            services.AddDatabaseDeveloperPageExceptionFilter();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; }
                )
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Issuer"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                );
            services
                .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()  // <- Broken AF
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
                {
                    options.AddPolicy("CorsAllowAll", builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
                }
            );

            
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBll, AppBll>();

            // services.AddRazorPages();
            services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomFloatingPointBinderProvider());
            });

            
            services.AddAutoMapper(
                typeof(AutoMapperProfile),
                typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(PublicApi.DTO.v1.MappingProfiles.AutoMapperProfile)
            );

            // add support for api versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            // add support for m2m api documentation
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });
            // add support to generate human readable documentation from m2m docs
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            
            var supportedCultures = Configuration
                .GetSection("SupportedCultures")
                .GetChildren()
                .Select(x => new CultureInfo(x.Value))
                .ToArray();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                // datetime and currency support
                options.SupportedCultures = supportedCultures;
                // UI translated strings
                options.SupportedUICultures = supportedCultures;
                // if nothing is found, use this
                options.DefaultRequestCulture = new RequestCulture("et-EE", "et-EE");
                options.SetDefaultCulture("et-EE");

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    // Order is important, its in which order they will be evaluated
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app">App builder</param>
        /// <param name="env">Host environment</param>
        /// <param name="apiVersionDescriptionProvider">Api version builder</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            SetupAppData(app, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        apiVersionDescription.GroupName.ToUpperInvariant()
                    );
                }
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseCors("CorsAllowAll");

            app.UseRouting();
            
            app.UseRequestLocalization(
                app.ApplicationServices
                    .GetService<IOptions<RequestLocalizationOptions>>()?.Value
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void SetupAppData(IApplicationBuilder app, IConfiguration configuration)
        {
            using var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (ctx != null)
            {
                // // in case of testing - dont do setup
                // if (ctx!.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") 
                //     return;

                
                if (configuration.GetValue<bool>("AppData:DropDatabase"))
                {
                    Console.Write("Drop database");
                    DataInit.DropDatabase(ctx);
                    Console.WriteLine(" - done");
                }

                if (configuration.GetValue<bool>("AppData:Migrate"))
                {
                    Console.Write("Migrate database");
                    DataInit.MigrateDatabase(ctx);
                    Console.WriteLine(" - done");
                }

                if (configuration.GetValue<bool>("AppData:SeedIdentity"))
                {
                    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

                    if (userManager != null && roleManager != null)
                    {
                        DataInit.SeedIdentity(userManager, roleManager, configuration);
                    }
                    else
                    {
                        Console.Write(
                            $"Err: User manager {(userManager == null ? "null" : "ok")}, role manager {(roleManager == null ? "null" : "ok")}!");
                    }
                }

                if (configuration.GetValue<bool>("AppData:SeedData"))
                {
                    Console.Write("Seed database");
                    DataInit.SeedAppData(ctx);
                    Console.WriteLine(" - done");
                }
            }

            //C# will dispose all the usings here
        }
    }
}