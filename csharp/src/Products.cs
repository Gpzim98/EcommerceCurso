using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Ecommerce.Products;

namespace Ecommerce.Products
{
    public class Products
    {
        private readonly ILogger _logger;

        public Products(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Products>();
        }

        [Function("Products")]
        public ProductBindings Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete")] 
        HttpRequestData req, ProductDTO product)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            if(req.Method == "GET") {  }    // get
            if(req.Method == "POST") { return Create(req, product); }
            if(req.Method == "PUT") {  return Update(req, product); }
            if(req.Method == "DELETE") {  } // delete

            return new ProductBindings
            {
                HttpResponse = response,
            };
        }

        private static ProductBindings Update(HttpRequestData req, ProductDTO product)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            if (string.IsNullOrEmpty(product.Id))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.WriteString("Product ID is required");
                var resp = new ProductBindings { HttpResponse = response };

                return resp;
            }

            response.WriteString($"Record {product.Id} updated");

            return new ProductBindings
            {
                HttpResponse = response,
                Product = product
            };
        }


        private static ProductBindings Create(HttpRequestData req, ProductDTO product)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            product.Id = Guid.NewGuid().ToString();
            response.WriteString($"{product.Id}");

            return new ProductBindings
            {
                HttpResponse = response,
                Product = product
            };
        }
    }
}
