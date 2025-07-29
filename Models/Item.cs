using Postgrest.Attributes;
using Postgrest.Models;

namespace Supabase_Minimal_API.Models
{
    [Table("items")]
    public class Item : BaseModel
    {
        [PrimaryKey("item_id")]
        public int ItemID { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public float Price { get; set; }

        [Column("supplier_id")]
        public long SupplierID { get; set; }
    }
}
