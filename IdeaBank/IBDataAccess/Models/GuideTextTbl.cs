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
        public int Id { get; set; }
        public string Purpose { get; set; }
        public string HomepageGuide { get; set; }
        public string SubmitGuide { get; set; }
    }
}
