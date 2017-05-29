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
    public class StockPrizeRepository : IRepository<DalStockPrize>
    {
        private readonly DbContext _context;

        public StockPrizeRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalStockPrize e)
        {
            var entity = e.ToOrm();
            _context.Set<stock_prize>().Add(entity);
        }

        public void Delete(DalStockPrize e)
        {
            var entity = _context.Set<stock_prize>().Single(en => en.id_stock_prize == e.Id);
            _context.Set<stock_prize>().Remove(entity);
        }

        public IEnumerable<DalStockPrize> GetAll()
        {
            return _context.Set<stock_prize>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalStockPrize> GetAllByPredicate(Expression<Func<DalStockPrize, bool>> f)
        {
            var criteria = ConvertExpression<DalStockPrize, stock_prize>.Convert(f, "OrmEntity");
            return _context.Set<stock_prize>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalStockPrize GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalStockPrize GetFirstByPredicate(Expression<Func<DalStockPrize, bool>> f)
        {
            var criteria = ConvertExpression<DalStockPrize, stock_prize>.Convert(f, "OrmEntity");
            var response = _context.Set<stock_prize>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalStockPrize e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<stock_prize>().Single(en => en.id_stock_prize == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
