using System;

namespace IBBusinessLogicLib
{
    public class Idea
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Initials { get; set; }
        public string Description { get; set; }
        public string BusinessUnit { get; set; }
        public string Department { get; set; }
        public string Risk { get; set; }
        public string Team { get; set; }
        public string PlanDescription { get; set; }
        public string ExpectedResults { get; set; }
        public bool IsHidden { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
