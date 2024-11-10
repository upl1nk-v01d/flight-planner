using flight_planner_net.Validations;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlannerService.Handlers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IValidator = flight_planner_net.Validations.IValidator;

namespace FlightPlannerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            
            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policy => {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.AddDbContext<FlightPlannerDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("flight-planner")));
            
            builder.Services.AddScoped<IDbService, DbService>();
            builder.Services.AddScoped<IDBClearingService, DbClearingService>();
            builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IValidator, CarrierValidator>();
            builder.Services.AddScoped<IValidator, FlightDatesValidator>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            builder.Services.AddValidatorsFromAssembly(executingAssembly);
            builder.Services.AddAutoMapper(executingAssembly);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}