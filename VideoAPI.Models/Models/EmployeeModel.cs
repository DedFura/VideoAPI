using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideoAPI.Models.Models {
    public class EmployeeModel {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Login { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Role { get; set; }
    }
}
