using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models.Context;
using ZarzadzanieDomem.Repositories;

namespace ZarzadzanieDomem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if DEBUG
            byte[] encoded = Convert.FromBase64String(Configuration.GetConnectionString("DockerDB"));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(System.Text.Encoding.UTF8.GetString(encoded), new MySqlServerVersion(new Version(8, 0, 21))));
#else       
            byte[] encoded = Convert.FromBase64String(Configuration.GetConnectionString("Production"));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(System.Text.Encoding.UTF8.GetString(encoded), new MariaDbServerVersion(new Version(10, 3, 27))));
#endif
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IAuthorizeRepository, AuthorizeRepository>();

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zarządzanie Domem ");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true) // allow any origin
             .AllowCredentials()); // allow credentials


            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
