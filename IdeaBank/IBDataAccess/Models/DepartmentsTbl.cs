using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLib.Models
{
    public class DepartmentsTbl
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [InverseProperty("IdeaDepartment")]
        public ICollection<IdeasTbl> IdeaIdeas { get; set; }
        [InverseProperty("AuthorDepartment")]
        public ICollection<IdeasTbl> AuthorIdeas { get; set; }

    }
}
