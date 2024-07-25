﻿using Ordering.Core.Models;
using Ordering.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T?> GetAsync(int id);  

        Task<IEnumerable<T>> GetAllAsync();







        Task<T?> GetWithSpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<int> GetCountAsync(ISpecification<T> spec);
    }
}
