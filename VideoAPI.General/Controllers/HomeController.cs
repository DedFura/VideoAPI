using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.Models.Models;
using VideoAPI.Services.Interfaces;

namespace VideoAPI.General.Controllers {
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<VideoModel> Get() {
            return _unitOfWork.Tasks.Get(1);
        }
    }
}