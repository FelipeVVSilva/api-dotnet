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

app.MapPost("/products", (Product product) =>
{
    ProductRepository.Add(product);
    return Results.Created("/products/" + product.Code, product.Code);
});

app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    Product prod = ProductRepository.GetByCode(code);
    if (prod != null)
        return Results.Ok(prod);
    return Results.NotFound();
});

app.MapPut("/products", (Product product) =>
{
    Product productSaved = ProductRepository.GetByCode(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok(productSaved);
});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    ProductRepository.DeleteProduct(code);
    return Results.NoContent();
});

app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetByCode(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void DeleteProduct(string code)
    {
        Product prod = GetByCode(code);
        Products.Remove(prod);
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
