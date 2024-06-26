﻿using BlazorWebTab.Server.Services.CategoryService;
using BlazorWebTab.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebTab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    /// <summary>
    /// List of all categories
    /// </summary>
    /// <returns>Category list</returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var result = await _categoryService.GetCategories();
        return Ok(result);
    }
}