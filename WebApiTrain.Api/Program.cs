using Serilog;
using WebApiTrain.Application.DependencyInjection;
using WebApiTrain.DAL.DependencyInjection;

namespace WebApiTrain.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSerilog();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwagger();

            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiTrain Swagger V 1.0");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebApiTrain Swagger V 2.0");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
