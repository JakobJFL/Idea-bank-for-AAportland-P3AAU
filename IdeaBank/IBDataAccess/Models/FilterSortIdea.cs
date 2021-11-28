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
    public class FilterSortIdea
    {
        public int Id { get; set; }
        public int BusinessUnit { get; set; }
        public int Department { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int CurrentPage { get; set; }
        public int IdeasShownCount { get; set; }
        public Sort Sorting { get; set; } = Sort.CreatedAtDesc;
        public bool ShowHidden { get; set; } = false;
        public string SearchStr { get; set; } = "";
        public bool OnlyNewIdeas { get; set; } = false;
    }
}