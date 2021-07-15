namespace SecondHandClothes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        public string CategoryName { get; set; }
    }
}
