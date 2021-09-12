using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MetricsAgent
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
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection, "cpumetrics");
            PrepareSchema(connection, "rammetrics");
            PrepareSchema(connection, "hddmetrics");
            PrepareSchema(connection, "dotnetmetrics");
            PrepareSchema(connection, "networkmetrics");

            FillTablesWithRandom(connection, "cpumetrics");
            connection.Close();
        }

        private void FillTablesWithRandom(SQLiteConnection connection, string tableName)
        {
            var rnd = new Random();
            using var command = new SQLiteCommand(connection);
            for (int i = 0; i < 100; i++)
            {
                command.CommandText = string.Format(
                    "INSERT INTO {0}(value,time) VALUES({1},\"{2}\");"
                    , tableName
                    , rnd.Next(100)
                    , new DateTime(2021, rnd.Next(1,13), rnd.Next(1,29)).ToString("u"));
                command.ExecuteNonQuery();
            }
        }

        private void PrepareSchema(SQLiteConnection connection,string tableName)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = string.Format("DROP TABLE IF EXISTS {0}",tableName);
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText = string.Format(@"CREATE TABLE {0}(
                    id INTEGER PRIMARY KEY
                    , value INT
                    , time DATETIME)",tableName);
                command.ExecuteNonQuery();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
