using Microsoft.EntityFrameworkCore;
using Tik_Tac_Toe.Buisnes;
using Tik_Tac_Toe.Buisnes.Services;
using Tik_Tac_Toe.Core.Abstractions;
using Tik_Tac_Toe.DataAccess;
using Tik_Tac_Toe.DataAccess.Repositories;

namespace Tik_Tac_Toe.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllers();
            //Repositories
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            //Services
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddSingleton<DataField>();

            var connectionString = builder.Configuration.GetConnectionString("GameDbContext");
            Console.WriteLine($"Using connection string: {connectionString}");
            builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameDbContext")));

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.MapGet("/health", () => Results.Ok());
            app.MapGet("/", () => Results.Redirect("/api/Game/GetGame"));
            app.MapControllers();

            app.Run();
        }
    }
}
