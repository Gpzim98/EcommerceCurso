using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json.Serialization;

namespace Ecommerce.Products
{
    public class ProductDTO 
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("Reference")]
        public string Reference { get; set; }
        [JsonPropertyName("Desc")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; } = false;
        public string Category { get; set; }
        public void SetAsDeleted()
        {
            this.Deleted = true;
        }
    }

    public class ProductBindings
    {
        public HttpResponseData HttpResponse { get; set; }
        
        [CosmosDBOutput("%DatabaseName%", "%ContainerName%", ConnectionStringSetting = "CosmosDBConnectionString", CreateIfNotExists=true)]
        public ProductDTO Product { get; set; }
    }
}