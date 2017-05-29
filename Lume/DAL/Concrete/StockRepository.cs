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
    public class StockRepository : IRepository<DalStock>
    {
        private readonly DbContext _context;

        public StockRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalStock e)
        {
            var entity = e.ToOrm();
            _context.Set<stock>().Add(entity);
        }

        public void Delete(DalStock e)
        {
            var entity = _context.Set<stock>().Single(en => en.id_stock == e.Id);
            _context.Set<stock>().Remove(entity);
        }

        public IEnumerable<DalStock> GetAll()
        {
            return _context.Set<stock>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalStock> GetAllByPredicate(Expression<Func<DalStock, bool>> f)
        {
            var criteria = ConvertExpression<DalStock, stock>.Convert(f, "OrmEntity");
            return _context.Set<stock>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalStock GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalStock GetFirstByPredicate(Expression<Func<DalStock, bool>> f)
        {
            var criteria = ConvertExpression<DalStock, stock>.Convert(f, "OrmEntity");
            var response = _context.Set<stock>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalStock e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<stock>().Single(en => en.id_stock == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
