using System;
using System.ComponentModel.DataAnnotations;

namespace TheLocal.Models {
    public class Post {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        [MaxLength(50)]
        public string User { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
