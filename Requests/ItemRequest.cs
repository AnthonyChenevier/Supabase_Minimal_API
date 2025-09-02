using Supabase_Minimal_API.Models;
using Supabase_Minimal_API.Validation;

namespace Supabase_Minimal_API.Requests;

/// <summary>
/// ItemRequest is used to serialise an ItemModel without a primary key. Used for formatting json requests with required fields.
/// </summary>
public class ItemRequest
{
    [ValidateStringLength("description", 20)]
    public string Description { get; set; }
    public float Price { get; set; }
    public long SupplierID { get; set; }

    /// <summary>
    /// Constructor to turn an ItemModel into an ItemRequest
    /// </summary>
    /// <param name="model"></param>
    public ItemRequest(ItemModel model)
    {
        Description = model.Description;
        Price = model.Price;
        SupplierID = model.SupplierID;
    }


    public ItemRequest()
    {
        Description = "none";
        Price = 0;
        SupplierID = 0;
    }
}