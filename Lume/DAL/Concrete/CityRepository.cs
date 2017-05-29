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
    public class CityRepository : IRepository<DalCity>
    {
        private readonly DbContext _context;

        public CityRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalCity e)
        {
            var entity = e.ToOrm();
            _context.Set<city>().Add(entity);
        }

        public void Delete(DalCity e)
        {
            var entity = _context.Set<city>().Single(en => en.id_city == e.Id);
            _context.Set<city>().Remove(entity);
        }

        public IEnumerable<DalCity> GetAll()
        {
            return _context.Set<city>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalCity> GetAllByPredicate(Expression<Func<DalCity, bool>> f)
        {
            var criteria = ConvertExpression<DalCity, city>.Convert(f, "OrmEntity");
            return _context.Set<city>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalCity GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalCity GetFirstByPredicate(Expression<Func<DalCity, bool>> f)
        {
            var criteria = ConvertExpression<DalCity, city>.Convert(f, "OrmEntity");
            var response = _context.Set<city>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalCity e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<city>().Single(en => en.id_city== e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
