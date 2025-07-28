using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Supabase_Minimal_API.Models
{
    [Table("items")]
    public class Item : BaseModel
    {
        [JsonProperty("item_id")]
        [PrimaryKey("item_id")]
        public int ItemID { get; set; }

        [JsonProperty("description")]
        [Column("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        [Column("price")]
        public float Price { get; set; }

        [JsonProperty("supplier_id")]
        [Column("supplier_id")]
        public long SupplierID { get; set; }
    }
}
