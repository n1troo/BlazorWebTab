﻿using System.Diagnostics;
using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebTab.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dbContext;

    public ProductService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        try
        {
            var response = new ServiceResponse<List<Product>>();
            var result
                = await _dbContext.Products
                    .Include(i => i.Variants)
                    .ToListAsync();

            if(result == null)
            {
                response.Success = false;
                response.Message = "No Product matches the given query";
            }
            else
            {
                response.Data = result;
                response.Success = true;
            }
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var response = new ServiceResponse<Product>
        {
            Data = await _dbContext.Products
                .Include(i => i.Variants)
                .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(p=>p.Id == productId),
            Success = true
        };

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dbContext.Products
                .Where(s => s.Category.Url.ToLower() == categoryUrl.ToLower())
                .Include(ii=>ii.Variants)
                .ToListAsync(),
            Success = true
        };
        return response;
    }

    public async Task<ServiceResponse<List<Product>>> SearchProduct(string searchText)
    {
        var responce = new ServiceResponse<List<Product>>()
        {
            Data = await FindProductsBySearchText(searchText)
        };

        return responce;
    }

    private async Task<List<Product>> FindProductsBySearchText(string searchText)
    {
        return await _dbContext.Products
            .Where(p=>p.Title.ToLower().Contains(searchText.ToLower()) 
                      || p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(s=>s.Variants)
            .ToListAsync();
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductsBySearchText(searchText);
        List<string> results = new List<string>();
        foreach (var product in products)
        {
            if(product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                results.Add(product.Title);

            if (product.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                var words = product.Description.Split().Select(s => s.Trim(punctuation));

                foreach (var word in words.Distinct())
                {
                    if(word.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        results.Add(word);
                }
                
            }
            
        }

        return new ServiceResponse<List<string>>(){Data = results, Success = true};

    }
}