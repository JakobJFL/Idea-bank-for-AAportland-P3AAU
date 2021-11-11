namespace BusinessLogicLib.Models
{
    public class NewIdea : Idea
    {
        public int BusinessUnit { get; set; } = 1;
        public int Department { get; set; } = 1;
    }
}
