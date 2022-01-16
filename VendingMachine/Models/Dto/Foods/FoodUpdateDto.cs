using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Dto
{
    [ExcludeFromCodeCoverage]
    public class FoodUpdateDto
    {
        [Required]
        public string productName { get; set; }

        [Required]
        public string productDesc { get; set; }

        [Required]
        public int productType { get; set; }

        [Required]
        public int? productMount { get; set; }

        [Required]
        public int? productPrice { get; set; }

        [Required]
        public string updatedBy { get; set; }
    }
}
