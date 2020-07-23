using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.General.Utility;
using VideoAPI.Models.Models;
using VideoAPI.Services.Interfaces;

namespace VideoAPI.General.Controllers {
    [ApiController]
    [Route("api")]
    public class VideosController : ControllerBase {

        private readonly IUnitOfWork _videoOfWork;
        private readonly IHostingEnvironment _hostingEnvironment;

        public VideosController(IUnitOfWork videOfWork, IHostingEnvironment hostingEnvironment)
        {
            _videoOfWork = videOfWork;
            _hostingEnvironment = hostingEnvironment;
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


        //[HttpPost]
        //[Route("addvideo")]
        //public Task<int> AddVideo(VideoModel addedVideo, IFormFile[] video) {
        //    if (video == null || video.Length == 0)
        //        return null;

        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    var files = HttpContext.Request.Form.Files;

            
        //    // 3. Проверяем, передана ли вообще картинка
        //    if (files.Count != 0) {

        //        // 4. Создаём путь сохранения картинки
        //        var uploads = Path.Combine(webRootPath, SD.VideosFolder);

        //        // 5. Получаем расширение переданного файла
        //        var extension = Path.GetExtension(files[0].FileName);

        //        // 6. Сохраняем видео
        //        using (var fileStream =
        //            new FileStream(Path.Combine(uploads, addedVideo.Id + extension), FileMode.Create)) {
        //            // Копируем изображение на сервер
        //            files[0].CopyTo(fileStream);
        //        }

        //        // Обновляем модель данных и добавляем в неё созданный путь
        //        addedVideo.Path = @"\" + SD.VideosFolder + @"\" + addedVideo.Id + extension;
        //    } else {
        //        // Формируем путь к изображению по умолчанию
        //        var uploads = Path.Combine(webRootPath, SD.VideosFolder + @"\" + SD.DefaultVideoName);

        //        // Копируем картинку по умолчанию в директорию конкретного продукта
        //        System.IO.File.Copy(uploads,
        //            webRootPath + @"\" + SD.VideosFolder + @"\" + addedVideo.Id + ".mp4");
        //    }
        //    // Сохраняем изменения в базу асинхронно

        //    // ----                           ----

        //    // Переадресовываем пользователя на страницу Products -> Index
        //    return _videoOfWork.Tasks.Add(addedVideo);

        //}

        [HttpDelete]
        [Route("deletevideo/{id}")]
        public Task<int> DeleteVideo(int id) {
            return _videoOfWork.Tasks.Delete(id);
        }
    }
}
