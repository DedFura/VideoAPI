using System;
using System.Collections.Generic;
using System.Text;
using VideoAPI.Services.Interfaces;

namespace TaskManagementApp.Infrastructure.Repository {
    public class UnitOfWork : IUnitOfWork {
        public UnitOfWork(ITaskRepository taskRepository) {
            Tasks = taskRepository;
        }
        public ITaskRepository Tasks { get; }
    }
}
