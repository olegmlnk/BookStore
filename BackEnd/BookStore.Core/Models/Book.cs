using System;

namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        private Book()
        { }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public static (Book Book, string Error) Create(string title, string description, decimal price)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title cannot be empty or longer than 250 symbols legnth";
            }

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Price = price
            };

            return (book, error);
        }
    }
}