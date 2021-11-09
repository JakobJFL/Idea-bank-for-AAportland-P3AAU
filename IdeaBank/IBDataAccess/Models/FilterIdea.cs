namespace DataBaseLib.Models
{
    public enum Sort
    {
        ProjectNameAsc,
        ProjectNameDesc,
        CreatedAtAsc,
        CreatedAtDesc,
        UpdatedAtAsc,
        UpdatedAtDesc
    }
    public class FilterIdea
    {
        public int BusinessUnit { get; set; }
        public int Department { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public Sort Sorting { get; set; } = Sort.CreatedAtDesc;
    }
}
