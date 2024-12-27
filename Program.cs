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

app.Run();

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
