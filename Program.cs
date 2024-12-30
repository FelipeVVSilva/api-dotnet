using System.Net;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/users", () => new { id = 1, name = "Felipe Veiga" });
app.MapGet("/addHeader", (HttpResponse response) =>
{
    response.Headers.Add("Teste", "Felipe Veiga");
    return new { id = 1, name = "Felipe Veiga" };
});
/*app.MapPost("/saveProduct", (Product product) =>
{
    //return product;
    return product.Code + " - " + product.Name; 
});*/

app.MapGet("/getProducts", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});

/*app.MapGet("/getProducts/{code}", ([FromRoute] string code) =>
{
    return code;
});*/

/*app.MapGet("/header", ([FromHeader] string headerName) =>
{
    return headerName;
});*/

app.MapGet("/getNameByHeader", (HttpRequest request) =>
{
    return request.Headers["headerName"].ToString();
});

app.MapPost("/saveProduct", (Product product) =>
{
    ProductRepository.Add(product);
});

app.MapGet("/getProductByCode/{code}", ([FromRoute] string code) =>
{
    return ProductRepository.GetByCode(code);
});

app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetByCode(string code)
    {
        Product prod = Products.First(p => p.Code == code);
        if(prod != null)
        {
            return Products.First(p => p.Code == code);
        }

        return null;
    }

}

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }

    public Product(){
        Code = null;
        Name = null;
    }
}
