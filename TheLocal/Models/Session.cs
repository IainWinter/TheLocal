using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheLocal.Models {
    public class Session {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string SessionId { get; set; }
    }
}
