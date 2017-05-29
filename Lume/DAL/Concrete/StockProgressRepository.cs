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
    public class StockProgressRepository :IRepository<DalStockProgress>
    {
        private readonly DbContext _context;

        public StockProgressRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalStockProgress e)
        {
            var entity = e.ToOrm();
            _context.Set<stock_progress>().Add(entity);
        }

        public void Delete(DalStockProgress e)
        {
            var entity = _context.Set<stock_progress>().Single(en => en.id_stock_progress == e.Id);
            _context.Set<stock_progress>().Remove(entity);
        }

        public IEnumerable<DalStockProgress> GetAll()
        {
            return _context.Set<stock_progress>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalStockProgress> GetAllByPredicate(Expression<Func<DalStockProgress, bool>> f)
        {
            var criteria = ConvertExpression<DalStockProgress, stock_progress>.Convert(f, "OrmEntity");
            return _context.Set<stock_progress>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalStockProgress GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalStockProgress GetFirstByPredicate(Expression<Func<DalStockProgress, bool>> f)
        {
            var criteria = ConvertExpression<DalStockProgress, stock_progress>.Convert(f, "OrmEntity");
            var response = _context.Set<stock_progress>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalStockProgress e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<stock_progress>().Single(en => en.id_stock_progress == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
