using Refit;
using RefitHttp.Dtos;

namespace RefitHttp;

public interface IPetStore
{
    [Get("/pet/findByStatus")]
    Task<IReadOnlyList<Pet>> FindByStatus([Query] string status);
}