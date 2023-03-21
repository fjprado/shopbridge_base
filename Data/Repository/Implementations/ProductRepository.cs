using Shopbridge_base.Data.Repository.Interfaces;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(Shopbridge_Context dbcontext) : base(dbcontext)
        {
        }
    }
}
