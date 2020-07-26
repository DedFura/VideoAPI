using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoAPI.General.Utility;
using VideoAPI.Models.Models;

namespace VideoAPI.General.Controllers {
    [Route("dashboard")]
    public class DashboardViewController : Controller {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public DashboardViewController(IWebHostEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("index")]
        public IActionResult Index() {
            return View();
        }

        [Route("showcategories")]
        public async Task<IActionResult> ShowAllCategories() {
            var categories = await GenericGetDataClass<List<CategoryModel>>.GetAllData("api/categories");
            return View(categories);
        }

        [Route("showcategory/{id}")]
        public async Task<IActionResult> ShowByIdCategories(int id) {
            var category = await GenericGetDataClass<CategoryModel>.GetAllData($"api/category/{id}");
            return View(category);
        }

        [HttpGet]
        [Route("editcategory/{id}")]
        public async Task<IActionResult> EditCategory(int id) {
            var category = await GenericGetDataClass<CategoryModel>.GetAllData($"api/category/{id}");
            return View(category);
        }

        [HttpPost]
        [Route("editcategory")]
        public async Task<IActionResult> EditCategory(CategoryModel editedCategory) {
            if (!ModelState.IsValid)
                return View(editedCategory);

            var response = await GenericGetDataClass<CategoryModel>.EditData("api/editcategory", editedCategory);

            if (response) {
                TempData["SM"] = "Категория успешно отредактирована!";
                return RedirectToAction(nameof(ShowAllCategories));
            } else
                return View(editedCategory);
        }

        [HttpGet]
        [Route("addcategory")]
        public IActionResult AddCategory() {
            return View();
        }

        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategory(CategoryModel addCategoryModel) {
            if (!ModelState.IsValid)
                return View(addCategoryModel);

            var response = await GenericGetDataClass<CategoryModel>.AddData("api/addcategory", addCategoryModel);
            if (response) {
                TempData["SM"] = "Категория успешно добавлена!";
                return RedirectToAction(nameof(ShowAllCategories));
            } else
                return View(addCategoryModel);
        }

        [Route("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var response = await GenericGetDataClass<CategoryModel>.DeleteData($"api/deletecategory/{id}");
            if (response) {
                TempData["SM"] = "Категория успешно удалена!";
                return RedirectToAction(nameof(ShowAllCategories));
            } else
                return View();
        }

        [Route("showvideos")]
        public async Task<IActionResult> ShowAllVideo() {
            var videos = await GenericGetDataClass<List<VideoModel>>.GetAllData("api/videos");
            return View(videos);
        }

        [Route("showvideo/{id}")]
        public async Task<IActionResult> ShowByIdVideos(int id) {
            var video = await GenericGetDataClass<VideoModel>.GetAllData($"api/video/{id}");
            return View(video);
        }

        [HttpGet]
        [Route("editvideo/{id}")]
        public async Task<IActionResult> EditVideo(int id) {
            var video = await GenericGetDataClass<VideoModel>.GetAllData($"api/video/{id}");
            return View(video);
        }

        [HttpPost]
        [Route("editvideo")]
        public async Task<IActionResult> EditVideo(VideoModel editedVideo) {
            if (!ModelState.IsValid)
                return View(editedVideo);

            var response = await GenericGetDataClass<VideoModel>.EditData("api/editvideo", editedVideo);

            if (response) {
                TempData["SM"] = "Видео успешно отредактировано!";
                return RedirectToAction(nameof(ShowAllVideo));
            } else
                return View(editedVideo);
        }

        [HttpGet]
        [Route("addvideo")]
        public async Task<IActionResult> AddVideo() {
            var categories = await GenericGetDataClass<List<CategoryModel>>.GetAllData("api/categories");
            ViewData["CategoryList"] = categories.ToList();
            return View();
        }

        [HttpPost]
        [Route("addvideo")]
        public async Task<IActionResult> AddVideo(VideoVM addedVideo, IFormFile[] file) {

            if (file == null || file.Length == 0) {
                ModelState.AddModelError("", "Видеофайл не выбран, пожалуйста, добавьте видео и попробуйте снова");
                return View(addedVideo);
            }

            #region Save video file
            if (file.Length == 1) {
                if (!ModelState.IsValid)
                    return View(addedVideo);

                // Формируем пути
                // Формируем путь к корневому каталогу сайта (wwwroot)
                var webRootPath = _hostingEnvironment.WebRootPath;

                // Получаем расширение файла
                var extension = Path.GetExtension(file[0].FileName);

                // Формируем полный абсолютный путь сохранения файла (с названием и расширением)
                var uploads = Path.Combine(webRootPath, SD.VideosFolder, addedVideo.VideoModel.Name + extension);

                // Формируем путь к видео файлу для модели и сохранения в базу
                var pathToVideo = Path.Combine("\\" + SD.VideosFolder, addedVideo.VideoModel.Name + extension);

                // Формируем пути для проверки, существуют ли каталоги для сохранения
                var directoryPath = Path.Combine(webRootPath, SD.VideosFolder);

                // Проверяем, существуют ли каталоги, если нет, то создаём
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                // Добавляем путь к видео в модель
                addedVideo.VideoModel.Path = pathToVideo;

                // Отправляем данные для сохранения в API контрроллер
                var response = await GenericGetDataClass<VideoVM>.AddData("api/addvideo", addedVideo);

                // Обрабатываем ответ, сохранена ли модель в базу, если да, сохраняем видео на сервер
                if (response) {
                    var stream = new FileStream(uploads, FileMode.Create);
                    await file[0].CopyToAsync(stream);
                    stream.Close();

                    TempData["SM"] = "Видео успешно добавлено!";
                    return RedirectToAction(nameof(ShowAllVideo));
                } else {
                    ModelState.AddModelError("", "Ошибка сохранения данных");
                    return View(addedVideo);
                }

                #endregion
            } else {
                var webRootPath = _hostingEnvironment.WebRootPath;
                var directoryPath = Path.Combine(webRootPath, SD.VideosFolder);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                foreach (var item in file) {
                    string extension = Path.GetExtension(item.FileName);
                    addedVideo.VideoModel.Name = String.Format(System.Guid.NewGuid() + extension);
                    addedVideo.VideoModel.CategoryId = 1;
                    var uploads = Path.Combine(webRootPath, SD.VideosFolder, addedVideo.VideoModel.Name + extension);
                    var pathToVideo = Path.Combine("\\" + SD.VideosFolder, addedVideo.VideoModel.Name + extension);
                    addedVideo.VideoModel.Path = pathToVideo;
                    var response = await GenericGetDataClass<VideoVM>.AddData("api/addvideo", addedVideo);

                    if (response) {
                        var stream = new FileStream(uploads, FileMode.Create);
                        await item.CopyToAsync(stream);
                        stream.Close();

                    } else {
                        ModelState.AddModelError("", "Ошибка сохранения данных");
                        return View(addedVideo);
                    }
                }
                TempData["SM"] = "Видео успешно добавлены!";
                return RedirectToAction(nameof(ShowAllVideo));

                // 2. Сохранить несколько видео

            }
        }


        [Route("deletevideo/{id}")]
        public async Task<IActionResult> DeleteVideo(int id) {

            // Получаем модель видео
            var model = await GenericGetDataClass<VideoModel>.GetAllData($"api/video/{id}");
            var response = await GenericGetDataClass<VideoModel>.DeleteData($"api/deletevideo/{id}");

            if (response) {

                // Формируем путь к корню сайта (wwwroot)
                var webRootPath = _hostingEnvironment.WebRootPath;

                // Получаем путь из модели и удаляем первый символ (/)
                var replacedPath = model.Path.Substring(1);

                // Формируем фактический путь к файлу для удаления
                var path = Path.Combine(webRootPath, replacedPath);

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                TempData["SM"] = "Видео успешно удалено!";
                return RedirectToAction(nameof(ShowAllVideo));
            } else
                // Реализовать вывод ошибки
                return RedirectToAction(nameof(ShowAllVideo));
        }

        [Route("showusers")]
        public async Task<IActionResult> ShowAllUsers() {
            var users = await GenericGetDataClass<IEnumerable<EmployeeModel>>.GetAllData("api/users");
            return View(users);
        }


        [HttpGet]
        [Route("edituser/{id}")]
        public async Task<IActionResult> EditUser(int id) {
            var user = await GenericGetDataClass<EmployeeModel>.GetAllData($"api/user/{id}");
            return View(user);
        }

        [Route("edituser")]
        public async Task<IActionResult> EditUser(EmployeeModel editedUser) {
            if (!ModelState.IsValid)
                return View(editedUser);
            var response = await GenericGetDataClass<EmployeeModel>.EditData("api/edituser", editedUser);

            if (response) {
                TempData["SM"] = "Пользователь успешно отредактирован!";
                return RedirectToAction(nameof(ShowAllUsers));
            } else
                return View(editedUser);
        }

        [Route("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            var response = await GenericGetDataClass<EmployeeModel>.DeleteData($"api/deleteuser/{id}");
            if (response) {
                TempData["SM"] = "Пользователь успешно удален!";
                return RedirectToAction(nameof(ShowAllUsers));
            } else
                return View();
        }

    }
}