using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Dto
{
    [ExcludeFromCodeCoverage]
    public class DrinkCreateDto
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDesc { get; set; }

        [Required]
        public int ProductType { get; set; }

        [Required]
        public int ProductMount { get; set; }

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public string CreatedBy { get; set; }

    }
}