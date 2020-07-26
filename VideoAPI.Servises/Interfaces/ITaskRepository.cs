using System.Collections.Generic;
using System.Threading.Tasks;
using VideoAPI.Models.Models;

namespace VideoAPI.Services.Interfaces {
    public interface ITaskRepository : IGenericRepository<VideoModel> {
        Task<int> Add(List<VideoModel> entity);
        Task<int> Add(VideoVM entity);
    }
}
