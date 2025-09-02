using Microsoft.AspNetCore.Mvc;
using Postgrest.Responses;
using Supabase_Minimal_API.Models;
using Supabase_Minimal_API.Requests;
using Supabase;

namespace Supabase_Minimal_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    // READ
    [HttpGet("{id:long}")]
    public async Task<IActionResult> OnGetAsync(long id, [FromServices] Client client)
    {
        ModeledResponse<ItemModel> response = await client.From<ItemModel>().Where(i => i.ItemID == id).Get();

        ItemModel? item = response.Model;

        if (item is null)
            return NotFound();

        return Ok(item);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> OnPostAsync([FromBody] ItemRequest request, [FromServices] Client client)
    {
        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);

        ItemModel item = new ItemModel(request);

        ModeledResponse<ItemModel> response = await client.From<ItemModel>().Insert(item);

        ItemModel newItem = response.Models.First();

        return Ok(newItem);
    }

    // UPDATE
    [HttpPut("{id:long}")]
    public async Task<IActionResult> OnPutAsync(long id, [FromBody] ItemRequest request, [FromServices] Client client)
    {
        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);

        ItemModel item = new ItemModel(id, request);

        ModeledResponse<ItemModel> response = await client.From<ItemModel>().Where(i => i.ItemID == id).Update(item);

        ItemModel? updatedItemResponse = response.Models.FirstOrDefault();

        if (updatedItemResponse is null)
            return NotFound();

        return Ok(updatedItemResponse);
    }
    
    // DELETE
    [HttpDelete]
    public async Task<IActionResult> OnDeleteAsync(long id, [FromServices] Client client)
    {
        await client.From<ItemModel>().Where(i => i.ItemID == id).Delete();

        return NoContent();
    }
}