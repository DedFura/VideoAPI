﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VideoAPI.Services.Interfaces {
    public interface IGenericRepository<T> where T : class {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(int id);
        Task<int> Add(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}