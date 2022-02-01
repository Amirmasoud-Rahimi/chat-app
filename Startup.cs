using ChatApp.Dao;
using ChatApp.Hubs;
using ChatApp.Services.Implements;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ChatApp
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Dependency Injection
            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<IAppService, AppService>();
            builder.Services.AddScoped<JwtManager,JwtManager>();

            // Database Context Config
            builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(@"Server=.;Database=MessagingDB;Trusted_Connection=True;"));
            
            // Jwt Config
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            
            // SignalR
            builder.Services.AddSignalR();

            // Add Services to the Container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger Config
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chat Web Application",
                    Description = "An ASP.NET Core Web API for Chat Application",
                    TermsOfService = new Uri("http://sanay.co/") 
                });

                // Add Jwt Authorization to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization Header using the Bearer Scheme.",
                    Name = "Jwt Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",                
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                   new OpenApiSecurityScheme {
                     Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                       }
                     },
                    new string[] {}
                        }
                      });
            });
        }

        private static void Configure(WebApplication app)
        {
            app.UseCors(builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            // Configure the HTTP Request Pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            
            //app.MapControllers();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }     
    }
}