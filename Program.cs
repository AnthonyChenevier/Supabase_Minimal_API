using Supabase_Minimal_API.Contracts;
using Supabase_Minimal_API.Models;
using Supabase;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(_ => new Client(builder.Configuration["https://uqoiqfwmxzhzbwopzeiq.supabase.co"],
                                           builder.Configuration["sb_publishable_T9r8VV1x35fCfHMmGezKAQ_2OM-eOaG"],
                                           new SupabaseOptions { AutoRefreshToken = true, AutoConnectRealtime = true }));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/items",
            async (CreateItemRequest request, Client client) =>
            {
                var item = new Item { Description = request.Description, Price = request.Price, SupplierID = request.SupplierID };
                var response = await client.From<Item>().Insert(item);
                var newItem = response.Models.First();

                return Results.Ok(newItem.ItemID);
            });

app.MapGet("/newsletters/{id}",
           async (long id, Client client) =>
           {
               var response = await client.From<Item>().Where(i => i.ItemID == id).Get();

               var item = response.Models.FirstOrDefault();

               if (item is null)
                   return Results.NotFound();

               var itemResponse = new ItemResponse { ItemID = item.ItemID, Description = item.Description, Price = item.Price, SupplierID = item.SupplierID };

               return Results.Ok(itemResponse);
           });

app.MapDelete("/newsletters/{id}",
              async (long id, Client client) =>
              {
                  await client.From<Item>().Where(i => i.ItemID == id).Delete();

                  return Results.NoContent();
              });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
