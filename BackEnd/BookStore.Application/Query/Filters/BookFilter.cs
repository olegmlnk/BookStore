namespace BookStore.Application.Query.Filters;

public class BookFilter
{
    public string? Title { get; set; }
    public decimal? Price { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}