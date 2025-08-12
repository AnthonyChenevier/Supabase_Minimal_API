using Newtonsoft.Json;
using Supabase;

namespace Supabase_Minimal_API;

internal static class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        bool useSwagger = builder.Configuration.GetValue<bool>("UseSwagger");

        ConfigureWebAppBuilder(builder, useSwagger);

        WebApplication app = BuildWebApp(builder, useSwagger);

        app.Run();
    }


    static WebApplication BuildWebApp(WebApplicationBuilder builder, bool useSwagger)
    {
        WebApplication app = builder.Build();

        if (useSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        return app;
    }

    private static void ConfigureWebAppBuilder(WebApplicationBuilder builder, bool useSwagger)
    {
        builder.Services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

        ConfigureSupabase(builder.Services, builder.Configuration);

        if (useSwagger)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }

    private static void ConfigureSupabase(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(_ =>
        {
            string? url = config.GetValue<string>("Supabase:Url");
            string? key = config.GetValue<string>("Supabase:Key");

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(key))
                throw new Exception("Missing Supabase configuration in appsettings.json.");

            SupabaseOptions options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            return new Client(url, key, options);
        });
    }
}