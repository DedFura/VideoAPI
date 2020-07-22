using System;
using System.Collections.Generic;
using System.Text;
using VideoAPI.Services.Interfaces;

namespace TaskManagementApp.Infrastructure.Repository {
    class UserOfWork : IEmployeeOfWork {
        public UserOfWork(IEmployeeRepository userRepository)
        {
            Users = userRepository;
        }
        public IEmployeeRepository Users { get; }
    }
}
