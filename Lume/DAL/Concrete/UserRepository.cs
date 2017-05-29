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
    public class UserRepository :IRepository<DalUser>
    {
        private readonly DbContext _context;

        public UserRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalUser e)
        {
            var entity = e.ToOrm();
            _context.Set<user>().Add(entity);
        }

        public void Delete(DalUser e)
        {
            var entity = _context.Set<user>().Single(en => en.id_user == e.Id);
            _context.Set<user>().Remove(entity);
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<user>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var criteria = ConvertExpression<DalUser, user>.Convert(f, "OrmEntity");
            return _context.Set<user>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalUser GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalUser GetFirstByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var criteria = ConvertExpression<DalUser, user>.Convert(f, "OrmEntity");
            var response = _context.Set<user>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalUser e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<user>().Single(en => en.id_user == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
