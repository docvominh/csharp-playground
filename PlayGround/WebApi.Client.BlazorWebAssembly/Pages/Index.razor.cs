using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using WebApi.Client.BlazorWebAssembly.Models;

namespace WebApi.Client.BlazorWebAssembly.Pages;

public partial class Index
{
    public Product? Product { get; set; }
    private List<Product>? Products { get; set; } = new();
    public bool ShowProductDialog;
    public string? Title { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }

    private void NewProduct()
    {
        Console.WriteLine("New Product");
        Product = new Product();
        Title = "New Product";
        ShowProductDialog = true;
    }

    private async Task ConfirmCreate()
    {
        Console.WriteLine("Log log");
        var productJson = JsonConvert.SerializeObject(Product, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var data = new StringContent(productJson, Encoding.UTF8, "application/json");
        var result = await HttpClient.PostAsync("http://localhost:5153/api/products", data);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            await Load();
            ShowProductDialog = false;
        }
    }

    private void EditProduct(Product selectedProduct)
    {
        Product = selectedProduct;
        Title = "Edit Product";
        ShowProductDialog = true;
    }

    private async Task ConfirmEdit()
    {
        var productJson = JsonConvert.SerializeObject(Product, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var data = new StringContent(productJson, Encoding.UTF8, "application/json");
        var result = await HttpClient.PutAsync("http://localhost:5153/api/products", data);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            await Load();
            ShowProductDialog = false;
        }
    }

    private async Task DeleteProduct(Product p)
    {
        var result = await HttpClient.DeleteAsync($"http://localhost:5153/api/products/{p.Id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            await Load();
            ShowProductDialog = false;
        }
    }

    private void Cancel()
    {
        ShowProductDialog = false;
    }

    private async Task Load()
    {
        Products = await HttpClient.GetFromJsonAsync<List<Product>>("http://localhost:5153/api/products") ??
                   new List<Product>();
    }
}