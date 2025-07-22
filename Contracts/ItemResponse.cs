// ItemResponse.cs
// 
// Part of Supabase_Minimal_API - Supabase_Minimal_API
// 
// Created by: Anthony on 2025/07/23 01:07
// Last edited by: Anthony on 2025/07/23 01:07


namespace Supabase_Minimal_API.Contracts;

public class ItemResponse
{
    public long ItemID { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public long SupplierID { get; set; }
}
