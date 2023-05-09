using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SourceAFIS;

namespace CQRSMediaTr.Models
{
    public class FingerprintTemplateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public byte[] TemplateData { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public static FingerprintTemplateModel FromTemplate(string userId, FingerprintTemplate template)
        {
            return new FingerprintTemplateModel
            {
                TemplateData = template.ToByteArray(),
                UserId = userId
            };
        }
    }
}