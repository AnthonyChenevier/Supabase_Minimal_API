using Supabase_Minimal_API.Contracts;
using Supabase_Minimal_API.Models;
using Supabase;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(_ =>
{
    string url = builder.Configuration["SupabaseUrl"];
    string supabaseKey = builder.Configuration["SupabaseApiKey"];

    SupabaseOptions options = new SupabaseOptions { AutoRefreshToken = true, AutoConnectRealtime = true };

    return new Client(url, supabaseKey, options);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Create
app.MapPost("/items",
            async (CreateItemRequest request, Client client) =>
            {
                var item = new Item { Description = request.Description, Price = request.Price, SupplierID = request.SupplierID };
                var response = await client.From<Item>().Insert(item);
                var newItem = response.Models.First();

                return Results.Ok(newItem.ItemID);
            });

//Read
app.MapGet("/items/{id}",
           async (long id, Client client) =>
           {
               var response = await client.From<Item>().Where(i => i.ItemID == id).Get();

               var item = response.Models.FirstOrDefault();

               if (item is null)
                   return Results.NotFound();

               var itemResponse = new ItemResponse { ItemID = item.ItemID, Description = item.Description, Price = item.Price, SupplierID = item.SupplierID };

               return Results.Ok(itemResponse);
           });

//Delete
app.MapDelete("/items/{id}",
              async (long id, Client client) =>
              {
                  await client.From<Item>().Where(i => i.ItemID == id).Delete();

                  return Results.NoContent();
              });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
