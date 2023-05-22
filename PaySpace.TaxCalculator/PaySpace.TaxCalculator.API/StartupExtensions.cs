

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PaySpace.TaxCalculator.API.Dto;
using PaySpace.TaxCalculator.API.Identity;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Models;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace PaySpace.TaxCalculator.API
{
    public static class StartupExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaxDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            }));
            services.Configure<SecurityServiceConfiguration>(configuration.GetSection("security"));
        }
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            
            services.AddScoped<IEnumerable<ITaxProcessor>>(options => GetInstances<ITaxProcessor>(serviceProvider));
        }

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            var setting = services.BuildServiceProvider().GetRequiredService<IOptions<SecurityServiceConfiguration>>();
            var signingKey = setting?.Value.JwtKey;
            var issuer = setting?.Value.JwtIssuer;
            var audience = setting?.Value.JwtAudience;

            services.AddTransient<IAuthorizationHandler, ValidTokenAuthorizationHandler>();
            services.AddAuthorization();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidIssuer = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientRegistration", policy =>
                {
                   policy.RequireRole(setting?.Value.Role);
                    policy.AddRequirements(new ValidTokenRequirement());
                });
            });

            services.AddHttpContextAccessor();

        }

        public static void AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TaxCalculator API",
                    Description = "TaxCalculator API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "BearerAuth"
                        }
                    },
                    Array.Empty<string>()
                }
            });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static void UseException(this WebApplication app)
        {
            var logger = app.Services.GetService<ILogger<WebApplication>>();
            app.UseExceptionHandler(option => {
                option.Run(async context => {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();
                    logger?.LogError(exception?.Error.ToString());
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        ErrorMessage = "An Error occurred while processing your request. Kindly try again later"
                    }));
                });
            });
        }

        public static void EnsureDatabaseSetup(this WebApplication application)
        {
            var context = application.Services.CreateScope().ServiceProvider.GetRequiredService<TaxDbContext>();
            
            if(context.Database.IsRelational())
                context.Database.Migrate();

            TaxDataSeeder.Seed(context);
        }

        private static List<T> GetInstances<T>(IServiceProvider serviceProvider)
        {
            var instances = new List<T>();
            var foundInstances = Assembly.GetAssembly(typeof(T))?.GetTypes()
                                ?.Where(detector => detector.IsClass &&
                                !detector.IsAbstract && typeof(T).IsAssignableFrom(detector));

            if (foundInstances != null && foundInstances.Any())
            {
                foreach (var type in foundInstances)
                {
                    var typeDetector = (T?)serviceProvider.GetService(type);
                    if (typeDetector != null)
                        instances.Add(typeDetector);
                }
            }
            return instances;
        }
    }
}
