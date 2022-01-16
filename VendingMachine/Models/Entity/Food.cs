using System;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class Food
    {
        public int id { get; set; }
        public string productName { get; set; }
        public int? productMount { get; set; }
        public int? productPrice { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public DateTime? creationDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int? deleted { get; set; }
        public string productDesc { get; set; }
    }
}
