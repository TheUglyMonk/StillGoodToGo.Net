using StillGoodToGo.Enum;
using System.ComponentModel.DataAnnotations;

namespace StillGoodToGo.Models
{
    public class Establishment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; } = Role.Establishment;

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public List<Category> Categories { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        [Range(0, 5)]
        public double Classification { get; set; }

        [Required]
        public Publications Publications { get; set; }
    }
}
