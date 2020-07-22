using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.Models.Models;
using VideoAPI.Services.Interfaces;

namespace VideoAPI.General.Controllers {
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase {
        private readonly IEmployeeOfWork _userOfWork;

        public UsersController(IEmployeeOfWork userOfWork)
        {
            _userOfWork = userOfWork;
        }

        [HttpGet]
        [Route("users")]
        public Task<IEnumerable<EmployeeModel>> GetAll()
        {
            return _userOfWork.Users.GetAll();
        }
    }
}
