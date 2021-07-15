namespace SecondHandClothes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Comment
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(CommentMaxLength)]
        public string CommentContent { get; set; }
    }
}
