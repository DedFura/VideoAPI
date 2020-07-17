using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAPI.Services.Interfaces {
    public interface IUnitOfWork {
        ITaskRepository Tasks { get; }
    }
}
