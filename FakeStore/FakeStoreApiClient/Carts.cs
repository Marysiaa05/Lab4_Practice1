using System.Text.Json.Serialization;

namespace FakeStoreApiClient
{
       public class Cart
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("products")]
        public List<CartProduct> Products { get; set; }
    }
    public class CartProduct
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

}
