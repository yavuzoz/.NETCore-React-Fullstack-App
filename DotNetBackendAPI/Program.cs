using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Personal_info_API.Dao;
using Personal_info_API.Data;
using System.IO;
using Personal_info_API.Data.Model;

namespace Personal_info_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SqlServerDB");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                options.AddPolicy("FrontEndClient", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                    .WithOrigins("http://localhost:3000"));
            });

            builder.Services.AddScoped<IUserDaoImp, UserDaoImp>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
                RequestPath = "/Image"
            });

            app.UseCors("FrontEndClient");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
