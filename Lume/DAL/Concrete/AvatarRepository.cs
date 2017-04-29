using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DAL.DalToOrmMappers;
using System.Data.Entity;
using ORM;
using CustomExpressionVisitor;

namespace DAL.Concrete
{
    public class AvatarRepository : IRepository<DalAvatar>
    {
        private readonly DbContext _context;

        public AvatarRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalAvatar e)
        {
            var entity = e.ToOrm();
            _context.Set<avatar>().Add(entity);
        }

        public void Delete(DalAvatar e)
        {
            var entity = _context.Set<avatar>().Single(en => en.id_Avatars == e.Id);
            _context.Set<avatar>().Remove(entity);
        }

        public IEnumerable<DalAvatar> GetAll()
        {
            return _context.Set<avatar>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalAvatar> GetAllByPredicate(Expression<Func<DalAvatar, bool>> f)
        {
            var criteria = ConvertExpression<DalAvatar, avatar>.Convert(f, "OrmEntity");
            return _context.Set<avatar>().Where(criteria.Compile()).ToList().Select(role => role.ToDal());
        }

        public DalAvatar GetById(int key)
        {
            return GetFirstByPredicate(role => role.Id == key);
        }

        public DalAvatar GetFirstByPredicate(Expression<Func<DalAvatar, bool>> f)
        {
            var criteria = ConvertExpression<DalAvatar, avatar>.Convert(f, "OrmEntity");
            var response = _context.Set<avatar>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalAvatar e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<avatar>().Single(r => r.id_Avatars == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
