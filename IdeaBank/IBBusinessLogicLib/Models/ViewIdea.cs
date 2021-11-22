using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib.Models
{
    public class ViewIdea : Idea
    {
        [Required]
        public int Id { get; set; }
        public string AuthorBusinessUnitStr { get; set; }
        public string AuthorDepartmentStr { get; set; }
        public string IdeaBusinessUnitStr { get; set; }
        public string IdeaDepartmentStr { get; set; }
        public string Risk { get; set; }
        public string PriorityStr { get; set; }
        public string StatusStr { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CommentsCount { get; set; }
    }
}
