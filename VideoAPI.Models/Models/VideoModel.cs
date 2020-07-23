using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideoAPI.Models.Models {
    public class VideoModel {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Path { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public bool IsCorrect { get; set; } = false;
    }
}
