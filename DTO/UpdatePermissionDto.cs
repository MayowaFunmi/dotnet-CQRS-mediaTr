using System.ComponentModel.DataAnnotations;

namespace CQRSMediaTr.DTO
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
    }
}