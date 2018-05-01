using System.ComponentModel.DataAnnotations;

namespace OurProject.Entities
{
    public enum standardstype
    {
        None,
        Type25,
        Type26,
        Type26s
    };

    public enum Accessories
    {
        None,
        Yes,
        No
    }

    public class Project
    {
        public int ProjectId { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string ProjectNumber { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Display(Name = "Maximum stock length")]
        public string StockLength { get; set; }

        [Display(Name = "Type of standards")]
        public standardstype StandardsType { get; set; }

        public Accessories AccessoriesList { get; set; }

        public string SuportBar { get; set; }

        public string TitleBlock { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string AttachmentName { get; set; }       
    }    
}
