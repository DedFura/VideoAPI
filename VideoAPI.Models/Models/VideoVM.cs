using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAPI.Models.Models {
    public class VideoVM {
        public VideoModel VideoModel { get; set; }
        public IEnumerable<CategoryModel> CategoryModel { get; set; }
    }
}
