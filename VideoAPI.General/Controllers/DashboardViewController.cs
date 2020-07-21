using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.Models.Models;

namespace VideoAPI.General.Controllers {
    [Route("dashboard")]
    public class DashboardViewController : Controller {
        [Route("index")]
        public IActionResult Index() {
            return View();
        }

        [Route("showall")]
        public async Task<IActionResult> ShowAllCategories()
        {
            var categories = await GenericGetDataClass<List<CategoryModel>>.GetAllData("api/categories");
            return View(categories);
        }

        [Route("showone/{id}")]
        public async Task<IActionResult> ShowByIdCategories(int id)
        {
            var category = await GenericGetDataClass<CategoryModel>.GetAllData($"api/category/{id}");
            return View(category);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditCategory(int id) {
            var category = await GenericGetDataClass<CategoryModel>.GetAllData($"api/category/{id}");
            return View(category);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditCategory(CategoryModel editedCategory)
        {
            if (!ModelState.IsValid)
                return View(editedCategory);

            var response = await GenericGetDataClass<CategoryModel>.EditData("api/edit", editedCategory);

            if (response) {
                TempData["SM"] = "Категория успешно отредактирована!";
                return RedirectToAction(nameof(ShowAllCategories));
            } else
                return View(editedCategory);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCategory(CategoryModel addCategoryModel)
        {
            if (!ModelState.IsValid)
                return View(addCategoryModel);

            var response = await GenericGetDataClass<CategoryModel>.AddData("api/add", addCategoryModel);
            if (response) {
                TempData["SM"] = "Категория успешно добавлена!";
                return RedirectToAction(nameof(ShowAllCategories));
            } else
                return View(addCategoryModel);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var response = await GenericGetDataClass<CategoryModel>.DeleteData($"api/delete/{id}");
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
        public IActionResult AddVideo() {
            return View();
        }

        [HttpPost]
        [Route("addvideo")]
        public async Task<IActionResult> AddVideo(VideoModel addedVideo) {
            if (!ModelState.IsValid)
                return View(addedVideo);

            var response = await GenericGetDataClass<VideoModel>.AddData("api/addvideo", addedVideo);
            if (response) {
                TempData["SM"] = "Видео успешно добавлено!";
                return RedirectToAction(nameof(ShowAllVideo));
            } else
                return View(addedVideo);
        }

        [Route("deletevideo/{id}")]
        public async Task<IActionResult> DeleteVideo(int id) {
            var response = await GenericGetDataClass<VideoModel>.DeleteData($"api/deletevideo/{id}");
            if (response) {
                TempData["SM"] = "Видео успешно удалено!";
                return RedirectToAction(nameof(ShowAllVideo));
            } else
                return View();
        }


    }
}