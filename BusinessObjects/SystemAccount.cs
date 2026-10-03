using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("SystemAccount")]
    public class SystemAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Không tự tăng
        public short AccountID { get; set; }

        [StringLength(100)]
        public string? AccountName { get; set; }

        [StringLength(70)]
        [EmailAddress]
        public string? AccountEmail { get; set; }

        public int? AccountRole { get; set; } // 1: Staff, 2: Lecturer

        [StringLength(70)]
        public string? AccountPassword { get; set; }

        public virtual ICollection<NewsArticle> NewsArticlesCreated { get; set; } = new List<NewsArticle>();
    }
}