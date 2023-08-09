namespace BethanysPieShop.Models
{
    public class Pie
    {
        //data types with ? are nullable and do not require a value
        public int PieId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? AllergyInformation { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }
        public bool IsPieOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }

        // ! after default is not null operator to indicate Category should not be null
        public Category Category { get; set; } = default!;


    }
}
