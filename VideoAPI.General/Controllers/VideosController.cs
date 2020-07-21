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
    public class VideosController : ControllerBase {

        private readonly IUnitOfWork _videoOfWork;

        public VideosController(IUnitOfWork videOfWork)
        {
            _videoOfWork = videOfWork;
        }

        [HttpGet]
        [Route("videos")]
        public Task<IEnumerable<VideoModel>> GetAll()
        {
            return _videoOfWork.Tasks.GetAll();
        }

        [HttpGet]
        [Route("video/{id}")]
        public Task<VideoModel> Get(int id)
        {
            return _videoOfWork.Tasks.Get(id);
        }

        [HttpPut]
        [Route("editvideo")]
        public Task<int> EditVideo(VideoModel editedVideo) {
            return _videoOfWork.Tasks.Update(editedVideo);
        }

        [HttpPost]
        [Route("addvideo")]
        public Task<int> AddVideo(VideoModel addedVideo) {
            return _videoOfWork.Tasks.Add(addedVideo);
        }

        [HttpDelete]
        [Route("deletevideo/{id}")]
        public Task<int> DeleteVideo(int id) {
            return _videoOfWork.Tasks.Delete(id);
        }
    }
}
