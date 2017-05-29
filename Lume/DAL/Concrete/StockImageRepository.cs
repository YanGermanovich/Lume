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
    public class StockImageRepository : IRepository<DalStockImage>
    {
        private readonly DbContext _context;

        public StockImageRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalStockImage e)
        {
            var entity = e.ToOrm();
            _context.Set<stock_and_image>().Add(entity);
        }

        public void Delete(DalStockImage e)
        {
            var entity = _context.Set<stock_and_image>().Single(en => en.id_stock_and_image== e.Id);
            _context.Set<stock_and_image>().Remove(entity);
        }

        public IEnumerable<DalStockImage> GetAll()
        {
            return _context.Set<stock_and_image>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalStockImage> GetAllByPredicate(Expression<Func<DalStockImage, bool>> f)
        {
            var criteria = ConvertExpression<DalStockImage, stock_and_image>.Convert(f, "OrmEntity");
            return _context.Set<stock_and_image>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalStockImage GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalStockImage GetFirstByPredicate(Expression<Func<DalStockImage, bool>> f)
        {
            var criteria = ConvertExpression<DalStockImage, stock_and_image>.Convert(f, "OrmEntity");
            var response = _context.Set<stock_and_image>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalStockImage e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<stock_and_image>().Single(en => en.id_stock_and_image == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
