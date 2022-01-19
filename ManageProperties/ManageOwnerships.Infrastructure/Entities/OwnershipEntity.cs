using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageOwnerships.Infrastructure.Entities
{
    public class OwnershipEntity
    {
        [Key]
        public int OwnershipId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string CodeInternal { get; set; }

        public int Year { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        [JsonIgnore]
        public virtual OwnerEntity Owner { get; set; }
        [JsonIgnore]
        public virtual ICollection<OwnershipImageEntity> OwnershipImages { get; set; }
        [JsonIgnore]
        public virtual ICollection<OwnershipTraceEntity> OwnershipTraces { get; set; }
    }
}
