using HM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HM.Clientes.API.Configuration
{
    public static class ApiConfig3
    {
        public static void AddApiConfiguration3(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<HMDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }

        public static void UseApiConfiguration3(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            //app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}