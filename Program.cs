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
app.MapPost("/saveProduct", (Product product) =>
{
    //return product;
    return product.Code + " - " + product.Name; 
});

app.MapGet("/getProducts", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});

app.MapGet("/getProducts/{code}", ([FromRoute] string code) =>
{
    return code;
});

/*app.MapGet("/header", ([FromHeader] string headerName) =>
{
    return headerName;
});*/

app.MapGet("/getNameByHeader", (HttpRequest request) =>
{
    return request.Headers["headerName"].ToString();
});

app.Run();

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
