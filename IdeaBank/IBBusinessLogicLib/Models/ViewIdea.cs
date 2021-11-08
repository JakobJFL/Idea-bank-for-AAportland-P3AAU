using System;
using System.Collections.Generic;

namespace BusinessLogicLib.Models
{
    public class ViewIdea : Idea
    {
        public int Id { get; set; }
        public string BusinessUnit { get; set; }
        public string Department { get; set; }
        public string Risk { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
