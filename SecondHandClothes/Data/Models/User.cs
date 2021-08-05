namespace SecondHandClothes.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
    }
}
