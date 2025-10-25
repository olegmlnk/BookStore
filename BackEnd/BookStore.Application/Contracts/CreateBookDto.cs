namespace BookStore.Contracts;

public class CreateBookDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}