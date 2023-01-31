﻿namespace BlazorWebTab.Shared.DTOs;

public class CartProductResponceDto
{
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
}