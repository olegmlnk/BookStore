using BookStore.Core.Abstractions;
using BookStore.Application.Services;
using BookStore.DataAccess;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", opts =>
                {
                    opts.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:5174", "https://localhost:5174");
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("BookStore.DataAccess"));
            });


            builder.Services.AddScoped<IBooksService, BooksService>();
            builder.Services.AddScoped<IBooksRepository, BooksRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("BookStore API");
                });
                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/scalar/", permanent: false);
                    return Task.CompletedTask;
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
