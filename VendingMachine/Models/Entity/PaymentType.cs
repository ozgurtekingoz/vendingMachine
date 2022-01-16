using System;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class PaymentType
    {
        public int id { get; set; }

        public string name { get; set; }

        public string createdBy { get; set; }

        public string updatedBy { get; set; }

        public DateTime? creationDate { get; set; }

        public DateTime? updateDate { get; set; }

        public int? deleted { get; set; }

        public string description { get; set; }
    }
}
