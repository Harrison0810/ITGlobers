using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageProperties.Infrastructure.Entities
{
    public class PropertyEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int IdProperty { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string CodeInternal { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }
        [Column(TypeName = "int")]
        public int Year { get; set; }
        [ForeignKey("IdOwner")]
        [Column(TypeName = "int")]
        public int IdOwner { get; set; }

        [JsonIgnore]
        public virtual OwnerEntity Owner { get; set; }
    }
}
