using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Dto
{
    [ExcludeFromCodeCoverage]
    public class DrinkUpdateDto
    {
        public string productName { get; set; }
        public string productDesc { get; set; }
        public int productType { get; set; }
        public int? productMount { get; set; }
        public int? productPrice { get; set; }
        public string updatedBy { get; set; }
    }
}
