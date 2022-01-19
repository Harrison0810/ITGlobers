using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageOwnerships.Infrastructure.Entities
{
    public class OwnershipTraceEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int OwnershipTraceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateSale { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Value { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }
        [ForeignKey("OwnershipId")]
        [Column(TypeName = "int")]
        public int OwnershipId { get; set; }

        [JsonIgnore]
        public virtual OwnershipEntity Ownership { get; set; }
    }
}
