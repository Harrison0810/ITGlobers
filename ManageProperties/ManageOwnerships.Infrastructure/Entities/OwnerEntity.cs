using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageOwnerships.Infrastructure.Entities
{
    public class OwnerEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int OwnerId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Photo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Birthday { get; set; }

        [JsonIgnore]
        public virtual ICollection<OwnershipEntity> Ownership { get; set; }
    }
}
