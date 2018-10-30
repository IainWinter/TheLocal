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
        //Has to be 32 bytes
        public byte[] Passcode { get; set; }

        [Required]
        [StringLength(15)]
        public string Salt { get; set; }
    }
}
