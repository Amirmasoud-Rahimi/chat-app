0- create Startup.cs and move config 
1- Add AppDbContext
  EntityFramework
  Microsoft.EntityFrameworkCore -> DbContext
  Microsoft.EntityFrameworkCore.SqlServer -> UseSqlServer
  tools
  https://medium.com/executeautomation/asp-net-core-6-0-minimal-api-with-entity-framework-core-69d0c13ba9ab
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=MessagingDB;Trusted_Connection=True;");
            }*/
        }
2- add Entities 
 -user
 -message
 3- Add services
4-Add controller

dependency injection
IEnumerable<WeatherForecast>
 [HttpGet(Name = "GetWeatherForecast")]