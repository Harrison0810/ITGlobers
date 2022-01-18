using System;

namespace ManageProperties.Infrastructure.Contracts
{
    public class PropertyTraceContract
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
    }
}
