namespace ManageOwnerships.Infrastructure.Contracts
{
    public class OwnershipImageContract
    {
        public int OwnershipImageId { get; set; }
        public int OwnershipId { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
