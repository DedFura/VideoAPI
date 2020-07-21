using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VideoAPI.Models.Models {
    public class CategoryModel {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }    
    }
}
