using System.ComponentModel.DataAnnotations;

namespace TheLocal.Models {
    public class User {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(84, MinimumLength = 84)]
        public string Passcode { get; set; }
    }
}
