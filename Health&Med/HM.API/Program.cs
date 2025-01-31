
using HM.Clientes.API.Configuration;
using MediatR;
using System.Reflection;
using TechChallenge.API.Configuration;

namespace HM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiConfiguration(builder.Configuration);

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.RegisterServices();

            builder.Services.AddMessageBusConfiguration(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            //ContatoEndpoints.Map(app);
            //ContatoEndpoints.Map1(app);
            //BuscarContatos.AddRoutes(app);
            //PrometheusEndpoints.Configure(app);

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1"); });

            app.ApplyMigrations();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
