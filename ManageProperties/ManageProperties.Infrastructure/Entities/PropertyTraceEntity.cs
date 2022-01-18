using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageProperties.Infrastructure.Entities
{
    public class PropertyTraceEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int IdPropertyTrace { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateSale { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Value { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }
        [ForeignKey("IdProperty")]
        [Column(TypeName = "int")]
        public int IdProperty { get; set; }

        [JsonIgnore]
        public virtual PropertyEntity Property { get; set; }
    }
}
