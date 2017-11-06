using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using COmpStore.Schema;
using COmpStore.Schema.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using COmpStore.Services;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Serilog;
using System.IO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using COmpStore.Repositories;

namespace COmpStore
{
    public class Startup
    {
        private readonly TokenValidationParameters _tokenValidationParameters;

        private void ConfigureAuth(IServiceCollection services)
        {
            ///////gddfg
        }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "Logging/log-{Date}.txt"))
            .CreateLogger();

            Configuration = builder.Build();

            _tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value)),
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("COmpStore")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<StoreDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            // Config authenticate service
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options => { options.TokenValidationParameters = _tokenValidationParameters; });
            //.AddCookie(options =>
            //{
            //    options.Cookie.Name = Configuration.GetSection("TokenAuthentication:CookieName").Value;
            //    options.TicketDataFormat = new CustomJwtDataFormat(
            //        SecurityAlgorithms.HmacSha256,
            //        _tokenValidationParameters);
            //});

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:60268");
                });
            });

            // Add transient
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();

            // AutoMapper
            services.AddAutoMapper();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My first API make by TraNguyen and LamPham", Version = "v1" });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.HttpMethod == null) return false;
                    return true;
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(configure =>
                {
                    configure.Run(async context =>
                    {
                        var ex = context.Features
                                        .Get<IExceptionHandlerFeature>()
                                        .Error;

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync($"{ex.Message}");
                    });
                });
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middle ware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Jwt Security Api v1 (DEBUG)");
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.WithOrigins("http://localhost:60268"));

            //app.UseAuthentication();
            app.UseStaticFiles();
           

            app.UseMvc();

            SampleData.InitializeMusicStoreDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
