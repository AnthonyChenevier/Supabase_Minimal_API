namespace Supabase_Minimal_API.Contracts
{
    public class CreateItemRequest
    {
        public string Description { get; set; }
        public float Price { get; set; }
        public long SupplierID { get; set; }
    }


    public record UpdateItemRequest(string Description, decimal Price, long SupplierID);
}
