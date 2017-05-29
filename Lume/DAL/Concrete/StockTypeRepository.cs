using CustomExpressionVisitor;
using DAL.DalToOrmMappers;
using DAL.DTO;
using DAL.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class StockTypeRepository : IRepository<DalStockType>
    {
        private readonly DbContext _context;

        public StockTypeRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalStockType e)
        {
            var entity = e.ToOrm();
            _context.Set<stock_type>().Add(entity);
        }

        public void Delete(DalStockType e)
        {
            var entity = _context.Set<stock_type>().Single(en => en.id_stock_type == e.Id);
            _context.Set<stock_type>().Remove(entity);
        }

        public IEnumerable<DalStockType> GetAll()
        {
            return _context.Set<stock_type>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalStockType> GetAllByPredicate(Expression<Func<DalStockType, bool>> f)
        {
            var criteria = ConvertExpression<DalStockType, stock_type>.Convert(f, "OrmEntity");
            return _context.Set<stock_type>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalStockType GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalStockType GetFirstByPredicate(Expression<Func<DalStockType, bool>> f)
        {
            var criteria = ConvertExpression<DalStockType, stock_type>.Convert(f, "OrmEntity");
            var response = _context.Set<stock_type>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalStockType e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<stock_type>().Single(en => en.id_stock_type == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
