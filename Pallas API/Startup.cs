using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Pallas_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pallas",
                    Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Please insert authorization token into field",
                    Name = "Authorization Token",
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddControllersWithViews();
            services.AddSession();

            services.AddDbContext<ApplicationDatabaseContext>(options => 
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], options => options.EnableRetryOnFailure()));

            services.AddAuthorization();
            services.AddAuthentication(authorization =>
            {
                authorization.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseRouting();
            application.UseSession();

            application.UseAuthentication();
            application.UseAuthorization();

            application.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");

                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.TryAdd("Authorization", "Bearer " + token);

                await next();
            });

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}