using Microsoft.AspNetCore.Mvc;

using Supabase_Minimal_API.Contracts;
using Supabase_Minimal_API.Models;

using Supabase;

namespace Supabase_Minimal_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    // Create
    [HttpPost("{id}")]
    public async Task<IActionResult> OnPostAsync([FromBody]CreateItemRequest request, [FromServices]Client client)
    {
        var item = new Item { Description = request.Description, Price = request.Price, SupplierID = request.SupplierID };
        var response = await client.From<Item>().Insert(item);
        var newItem = response.Models.First();

        return Ok(newItem.ItemID);
    }
    
    // READ
    [HttpGet("{id}")]
    public async Task<IActionResult> OnGetAsync(int id, [FromServices]Client client)
    {
        var response = await client.From<Item>().Where(i => i.ItemID == id).Get();

        var item = response.Models.FirstOrDefault();

        if (item is null)
            return NotFound();

        var itemResponse = new ItemResponse { ItemID = item.ItemID, Description = item.Description, Price = item.Price, SupplierID = item.SupplierID };

        return Ok(itemResponse);
    }
    
    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> OnPutAsync(int id, [FromBody]Item updatedItem, [FromServices]Client client)
    {
        updatedItem.ItemID = id;
        var response = await client.From<Item>().Where(i => i.ItemID == id).Update(updatedItem);

        var updatedItemResponse = response.Models.FirstOrDefault();

        if (updatedItemResponse is null)
            return NotFound();

        return Ok(updatedItemResponse);
    }
    
    // DELETE
    [HttpDelete]
    public async Task<IActionResult> OnDeleteAsync(int id, [FromServices]Client client)
    {
        await client.From<Item>().Where(i => i.ItemID == id).Delete();

        return NoContent();
    }
}