using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDataAccessLib.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(5)]
        public string Initials { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
