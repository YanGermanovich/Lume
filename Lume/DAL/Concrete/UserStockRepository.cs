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
    public class UserStockRepository : IRepository<DalUserStock>
    {
        private readonly DbContext _context;

        public UserStockRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalUserStock e)
        {
            var entity = e.ToOrm();
            _context.Set<user_and_stock>().Add(entity);
        }

        public void Delete(DalUserStock e)
        {
            var entity = _context.Set<user_and_stock>().Single(en => en.id_User_and_Stock == e.Id);
            _context.Set<user_and_stock>().Remove(entity);
        }

        public IEnumerable<DalUserStock> GetAll()
        {
            return _context.Set<user_and_stock>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalUserStock> GetAllByPredicate(Expression<Func<DalUserStock, bool>> f)
        {
            var criteria = ConvertExpression<DalUserStock, user_and_stock>.Convert(f, "OrmEntity");
            return _context.Set<user_and_stock>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalUserStock GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalUserStock GetFirstByPredicate(Expression<Func<DalUserStock, bool>> f)
        {
            var criteria = ConvertExpression<DalUserStock, user_and_stock>.Convert(f, "OrmEntity");
            var response = _context.Set<user_and_stock>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalUserStock e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<user_and_stock>().Single(en => en.id_User_and_Stock == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
