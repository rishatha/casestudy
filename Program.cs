
using CareerConnect.Data;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using CareerConnect.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            /*builder.Services.AddDbContext<AppDbContext>();*/ //hardcoded

            //json
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            /* //validator

             builder.Services.AddFluentValidationAutoValidation();
             builder.Services.AddValidatorsFromAssemblyContaining<ApplicationValidator>();*/


            //auth
            builder.Services.AddScoped<IAuthService, AuthService>();



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


            app.MapControllers();

            app.Run();
        }
    }
}
