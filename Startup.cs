using ChatApp.Dao;
using ChatApp.Services.Implements;
using ChatApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp //A
{
    public static class Startup // Why static class?
    {
        public static WebApplication InitializeApp(string[] args) //args
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;             
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            //A Dependency Injection
            builder.Services.AddScoped<IAppRepository, AppRepository>(); //Repository
            builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(@"Server=.;Database=MessagingDB;Trusted_Connection=True;"));//dbContext injection

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}