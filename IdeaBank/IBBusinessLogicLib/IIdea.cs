namespace BusinessLogicLib
{
    interface IIdea
    {
        public string ProjectName { get; set; }
        public string Initials { get; set; }
        public string Description { get; set; }
        public string Team { get; set; }
        public string Plan { get; set; }
        public string ExpectedResults { get; set; }
    }
}
