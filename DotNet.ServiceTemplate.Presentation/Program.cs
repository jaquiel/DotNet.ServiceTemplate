using DotNet.ServiceTemplate.Domain;
using DotNet.ServiceTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.ServiceTemplate.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseInMemoryDatabase("Persons");
            //});

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.BuildServiceProvider()
                            .GetService<DataContext>()?.Database.EnsureCreated();

            builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();
            builder.Services.AddScoped<IService<Person>, Service<Person>>();

            builder.Services.AddControllers();
            builder.Services.AddApiVersioning(version =>
            {
                version.DefaultApiVersion = new ApiVersion(2,0);
                version.AssumeDefaultVersionWhenUnspecified = true;
                version.ReportApiVersions = true;

            }).AddEndpointsApiExplorer();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
