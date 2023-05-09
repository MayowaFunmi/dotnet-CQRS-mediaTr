// to extend identity user

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CQRSMediaTr.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public byte[] FingerprintTemplate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set;}
    }
}