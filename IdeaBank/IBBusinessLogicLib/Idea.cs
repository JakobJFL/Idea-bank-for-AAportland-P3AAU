﻿using System;

namespace BusinessLogicLib
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
        public string Plan { get; set; }
        public string ExpectedResults { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
