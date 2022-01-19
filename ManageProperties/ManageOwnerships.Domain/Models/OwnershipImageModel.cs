namespace ManageOwnerships.Domain.Models
{
    public class OwnershipImageModel
    {
        public int OwnershipImagenId { get; set; }
        public int OwnershipId { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
