using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib
{
    public class NewIdea : Idea
    {
        public int BusinessUnit { get; set; } = 1;
        public int Department { get; set; } = 1;
        public bool IsHidden { get; set; }
    }
}
