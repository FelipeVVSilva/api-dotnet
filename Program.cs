using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

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
