// See https://aka.ms/new-console-template for more information

using Refit;
using RefitHttp;

Console.WriteLine("Hello, World!");
var petStoreApi = RestService.For<IPetStore>("https://petstore.swagger.io/v2");

var pets = await petStoreApi.FindByStatus("available");

foreach (var pet in pets)
{
    Console.WriteLine($"{pet.Id}/{pet.Name}");
}