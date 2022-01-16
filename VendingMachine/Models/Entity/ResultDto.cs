using System;
using System.Diagnostics.CodeAnalysis;

namespace RestApiSample.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class ResultDto
    {
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }

    }
}
