using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem
{
    public class Startup
    {
        private const string CorsPolicyName = "MyPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DockerDB"), new MySqlServerVersion(new Version(8, 0, 21))));
#else
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("Production"), new MariaDbServerVersion(new Version(10, 3, 27))));
#endif
            services.AddControllers();

            services.AddSwaggerGen();
            services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>

            {

                builder.AllowAnyOrigin()

                       .AllowAnyMethod()

                       .AllowAnyHeader();

            }));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
