using System;

namespace BusinessLogicLib
{
    public class Comment
    {
        public int Id { get; set; }
        public string Initials { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdeaID { get; set; }

    }
}
