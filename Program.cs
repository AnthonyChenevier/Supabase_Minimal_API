using Newtonsoft.Json;
using Supabase;

namespace Supabase_Minimal_API;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        var config = builder.Configuration;

        var useSwagger = config.GetValue<bool>("UseSwagger");
        if (useSwagger)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        ConfigureSupabase(builder.Services, config);

        var app = builder.Build();

        if (useSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureSupabase(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(_ =>
        {
            string? url = config.GetValue<string>("SupabaseUrl");
            string? supabaseKey = config.GetValue<string>("SupabaseApiKey");

            if (string.IsNullOrWhiteSpace(url) ||
                string.IsNullOrWhiteSpace(supabaseKey))
            {
                throw new Exception("Missing Supabase configuration in appsettings.json.");
            }

            SupabaseOptions options = new SupabaseOptions { AutoRefreshToken = true, AutoConnectRealtime = true };

            return new Client(url, supabaseKey, options);
        });
    }
}
