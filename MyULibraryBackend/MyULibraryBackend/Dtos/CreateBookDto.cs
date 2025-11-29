namespace MyULibraryBackend.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int IdGenre { get; set; }
        public int Quantity { get; set; }
    }
}
