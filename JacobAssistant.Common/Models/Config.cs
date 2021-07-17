using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class Config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "varchar(200)")]

        public string Name { get; set; }
        public string Value { get; set; }
    }
}