using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.Services.Interfaces;
using VideoAPI.Models.Models;

namespace VideoAPI.General.Controllers {
    [ApiController]
    [Route("api")]
    public class CategoryesController : ControllerBase {
        private readonly ICategoryOfWork _categoryOfWork;
        public CategoryesController(ICategoryOfWork categoryOfWork) {
            _categoryOfWork = categoryOfWork;
        }

        [HttpGet]
        [Route("categories")]
        public Task<IEnumerable<CategoryModel>> GetAll()
        {
            return _categoryOfWork.Categoryes.GetAll();
        }

        [HttpGet]
        [Route("category/{id}")]
        public Task<CategoryModel> Get(int id) {
            return _categoryOfWork.Categoryes.Get(id);
        }

        [HttpPut]
        [Route("editcategory")]
        public Task<int> EditCategory(CategoryModel editedCategory)
        {
            return _categoryOfWork.Categoryes.Update(editedCategory);
        }

        [HttpPost]
        [Route("addcategory")]
        public Task<int> AddCategory(CategoryModel addedCategory)
        {
            return _categoryOfWork.Categoryes.Add(addedCategory);
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        public Task<int> DeleteCategory(int id)
        {
            return _categoryOfWork.Categoryes.Delete(id);
        }
    }
}