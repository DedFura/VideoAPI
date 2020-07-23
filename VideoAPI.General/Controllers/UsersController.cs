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

        [HttpGet]
        [Route("user/{id}")]
        public Task<EmployeeModel> Get(int id)
        {
            return _userOfWork.Users.Get(id);
        }

        [HttpPut]
        [Route("edituser")]
        public Task<int> EditUser(EmployeeModel editedUser)
        {
            return _userOfWork.Users.Update(editedUser);
        }

        [HttpDelete]
        [Route("deleteuser/{id}")]
        public Task<int> DeleteUser(int id)
        {
            return _userOfWork.Users.Delete(id);
        }
    }
}
