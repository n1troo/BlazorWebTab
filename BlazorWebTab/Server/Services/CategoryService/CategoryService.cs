using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebTab.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Category>>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        var result = new ServiceResponse<List<Category>>();
        if (categories != null)
        {
            result.Data = categories;
            result.Success = true;
        }
        else
        {
            result.Success = false;
            result.Message = "Can't find any category.";
        }

        return result;
    }
}