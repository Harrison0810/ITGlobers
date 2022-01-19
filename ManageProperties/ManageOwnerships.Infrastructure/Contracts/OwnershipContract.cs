namespace ManageOwnerships.Infrastructure.Contracts
{
    public class OwnershipContract
    {
        public int OwnershipId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CodeInternal { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int OwnerId { get; set; }
    }
}
