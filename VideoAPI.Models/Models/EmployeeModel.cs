using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAPI.Models.Models {
    public class EmployeeModel {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
