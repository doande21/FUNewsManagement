using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("NewsArticle")]
    public class NewsArticle
    {
        [Key]
        [StringLength(20)]
        public string NewsArticleID { get; set; } = null!;

        [StringLength(400)]
        public string? NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(150)]
        public string Headline { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        [StringLength(4000)]
        public string? NewsContent { get; set; }

        [StringLength(400)]
        public string? NewsSource { get; set; }

        public short? CategoryID { get; set; }
        public bool? NewsStatus { get; set; }
        public short? CreatedByID { get; set; }
        public short? UpdatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual SystemAccount? CreatedBy { get; set; }

        // EF Core 6/7/8 tự động map bảng trung gian NewsTag
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}