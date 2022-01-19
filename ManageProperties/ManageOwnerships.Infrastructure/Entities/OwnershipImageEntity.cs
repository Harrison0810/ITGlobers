using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageOwnerships.Infrastructure.Entities
{
    public class OwnershipImageEntity
    {
        [Key]
        public int OwnershipImageId { get; set; }

        [ForeignKey("OwnershipId")]
        public int OwnershipId { get; set; }

        public string File { get; set; }

        public bool Enabled { get; set; }

        [JsonIgnore]
        public virtual OwnershipEntity Ownership { get; set; }
    }
}
