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
        //16 bytes long always
        public byte[] SessionId { get; set; }
    }
}
