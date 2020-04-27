using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotesKeeper.BusinessLayer;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.DataAccess.EntityFramework;
using NotesKeeper.DataAccess.NoSQL;
using NotesKeeper.WebApi.Framework;
using NotesKeeper.WebApi.Framework.Helper;
using SimpleInjector;
using System;
using System.IO;
using System.Reflection;

namespace NotesKeeper.WebApi
{
    public class Startup
    {
        private Container container;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            container = new Container();
            container.Options.ResolveUnregisteredConcreteTypes = false;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddJWTAuth(Configuration);

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NotesKeeper.WebApi",
                    Version = "v1"
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddSimpleInjector(container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope and
                // allows request-scoped framework services to be resolved.
                options.AddAspNetCore()
                    // Ensure activation of a specific framework type to be created by
                    // Simple Injector instead of the built-in configuration system.
                    // All calls are optional. You can enable what you need. For instance,
                    // PageModels and TagHelpers are not needed when you build a Web API.
                    .AddControllerActivation()
                    .AddViewComponentActivation();
                    //.AddPageModelActivation()
                    //.AddTagHelperActivation();

                // Optionally, allow application components to depend on the non-generic
                // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
                // (Microsoft.Extensions.Localization) abstractions.
                options.AddLogging();
                //options.AddLocalization();
            });

            InitializeContainer();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ConnectionStringExtractor>();

            var connection = Configuration.GetConnectionString("MainConnection");
            services.AddDbContext<IDbContext, MainDbContext>(options =>
            { 
                options.UseSqlServer(connection);
            }, ServiceLifetime.Scoped);

            var defaultUserConnection = Configuration.GetConnectionString("DefaultUserConnection");
            services.AddDbContext<UserDbContext>((provider, builder) => 
            {
                var factory = (ConnectionStringExtractor)provider.GetService(typeof(ConnectionStringExtractor));
                var connectionString = factory.GetUserConnectionString();

                builder.UseSqlServer(connectionString ?? defaultUserConnection);
            }, ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSimpleInjector(container);
            container.Verify();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeContainer()
        {
            BusinessLayerBootstraper.Bootstrap(container);
            DataAccesNoSqlBootstraper.Bootstrap(container);
            DataAccessEFBootstraper.Bootstrap(container);
        }
    }
}
