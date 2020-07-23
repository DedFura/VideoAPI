using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.General.Utility;
using VideoAPI.Models.Models;

namespace VideoAPI.General.Controllers {
    [Route("dashboard")]
    public class DashboardViewController : Controller {


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