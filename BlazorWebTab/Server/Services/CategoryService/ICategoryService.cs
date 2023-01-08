using BlazorWebTab.Shared;

namespace BlazorWebTab.Server.Services.CategoryService;

public interface ICategoryService
{
    public Task<ServiceResponse<List<Category>>> GetCategories();
}