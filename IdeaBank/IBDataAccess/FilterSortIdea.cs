using System.ComponentModel.DataAnnotations;

namespace DataBaseLib
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
        [Required]
        public int Id { get; set; }
        [Required]
        public int BusinessUnit { get; set; }
        [Required]
        public int Department { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int IdeasShownCount { get; set; }
        [Required]
        public Sort Sorting { get; set; } = Sort.CreatedAtDesc;
        [Required]
        public bool ShowHidden { get; set; } = false;
        [Required]
        public string SearchStr { get; set; } = "";
        [Required]
        public bool OnlyNewIdeas { get; set; } = false;
    }
}
