using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageProperties.Infrastructure.Entities
{
    public class PropertyImageEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int IdPropertyImagen { get; set; }
        [ForeignKey("IdProperty")]
        [Column(TypeName = "int")]
        public int IdProperty { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string File { get; set; }
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public virtual PropertyEntity Property { get; set; }
    }
}
