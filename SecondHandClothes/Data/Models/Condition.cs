namespace SecondHandClothes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Condition
    {
        public int Id { get; init; }

        [Required]
        public string ConditionType { get; set; }
    }
}
