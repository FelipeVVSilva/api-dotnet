public static class ProductRepository
{
    public static List<Product> Products { get; set; } = new List<Product>();

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