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
    public class PrizeRepository : IRepository<DalPrize>
    {
        private readonly DbContext _context;

        public PrizeRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalPrize e)
        {
            var entity = e.ToOrm();
            _context.Set<prize>().Add(entity);
        }

        public void Delete(DalPrize e)
        {
            var entity = _context.Set<prize>().Single(en => en.id_prize == e.Id);
            _context.Set<prize>().Remove(entity);
        }

        public IEnumerable<DalPrize> GetAll()
        {
            return _context.Set<prize>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalPrize> GetAllByPredicate(Expression<Func<DalPrize, bool>> f)
        {
            var criteria = ConvertExpression<DalPrize, prize>.Convert(f, "OrmEntity");
            return _context.Set<prize>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalPrize GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalPrize GetFirstByPredicate(Expression<Func<DalPrize, bool>> f)
        {
            var criteria = ConvertExpression<DalPrize, prize>.Convert(f, "OrmEntity");
            var response = _context.Set<prize>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalPrize e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<prize>().Single(en => en.id_prize == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
