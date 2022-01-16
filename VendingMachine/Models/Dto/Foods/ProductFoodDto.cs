using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Dto
{
    [ExcludeFromCodeCoverage]
    public class ProductFoodDto
    {
        [Required]
        public int productId { get; set; }
        [Required]
        public int productMount { get; set; }
        [Required]
        public int productPrice { get; set; }
        [Required]
        public int paymentType { get; set; }
        [Required]
        public string createdBy { get; set; }
    }
}
