namespace BlazorWebTab.Shared.DTO_s;

public class ProductSearchResultsDTO
{
    private List<Product> Products { get; set; } = new List<Product>();
    
    public int Page { get; set; }
    public int CurrentPage { get; set; }
    

}