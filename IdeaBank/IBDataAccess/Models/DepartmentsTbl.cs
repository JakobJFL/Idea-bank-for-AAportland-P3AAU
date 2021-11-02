﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLib.Models
{
    public class DepartmentsTbl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        public ICollection<IdeasTbl> Ideas { get; set; }
    }
}
