using System;

namespace ManageOwnerships.Infrastructure.Contracts
{
    public class OwnershipTraceContract
    {
        public int OwnershipTraceId { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal Tax { get; set; }
        public int OwnershipId { get; set; }
    }
}
