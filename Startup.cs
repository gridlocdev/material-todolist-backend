using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Todo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace material_todolist_backend
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


            /**
            Begin Adding SQLite Service Parameters
            **/
            var connectionString = new SqliteConnectionStringBuilder(Configuration.GetConnectionString("SqliteConnectionString"))
            {
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            services.AddDbContext<TodoContext>(
                options =>
                options.UseSqlite(connectionString)
            );

            /**
            End Adding SQLite Service Parameters
            **/

            services.AddScoped<ITodoSqliteRepo, TodoSqliteRepo>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "material_todolist_backend", Version = "v1" });
            });



            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "material_todolist_backend v1"));
            }


            app.UseRouting();

            // Use the CORS policy
            app.UseCors("foo"); // second

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
