using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLib.Models
{
    public class GuideTextTbl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Purpose { get; set; }
        [MaxLength(1000)]
        public string HomepageGuide { get; set; }
        [MaxLength(1000)]
        public string SubmitGuide { get; set; }
    }
}
