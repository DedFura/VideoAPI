using VideoAPI.Services.Interfaces;

namespace TaskManagementApp.Infrastructure.Repository {
    class CategoryOfWork : ICategoryOfWork {
        public CategoryOfWork(ICategoryRepository categoryRepository)
        {
            Categoryes = categoryRepository;
        }

        public ICategoryRepository Categoryes { get; }
    }
}
