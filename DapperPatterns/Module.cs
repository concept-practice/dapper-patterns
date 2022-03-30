using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using DapperPatterns.AircraftTypes;
using DapperPatterns.Airplanes;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;
using DapperPatterns.Messaging;
using Microsoft.EntityFrameworkCore;

namespace DapperPatterns
{
    public static class Module
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped(_ => new SqlConnection("Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=SierraNevadaWestern;Integrated Security=True"));

            services.AddDbContext<Context>(builder =>
                builder.UseSqlServer(
                    "Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=SierraNevadaWestern;Integrated Security=True"));

            services.AddTransient<IAirplaneRepository, AirplaneRepository>();
            services.AddTransient<SqlBuilder<Airplane, Guid>, AirplaneSqlBuilder>();
            services.AddTransient<AirplaneSqlBuilder>();
            services.AddScoped<IBus, Bus>();
            services.AddTransient<IAircraftTypeRepository, SimpleAircraftTypeRepository>();
            services.AddTransient<SqlBuilder<AircraftType, Guid>, AircraftTypeSqlBuilder>();
        }
    }
}
