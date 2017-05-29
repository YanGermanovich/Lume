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
    public class CountryRepository :IRepository<DalCountry>
    {
        private readonly DbContext _context;

        public CountryRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalCountry e)
        {
            var entity = e.ToOrm();
            _context.Set<country>().Add(entity);
        }

        public void Delete(DalCountry e)
        {
            var entity = _context.Set<country>().Single(en => en.id_country == e.Id);
            _context.Set<country>().Remove(entity);
        }

        public IEnumerable<DalCountry> GetAll()
        {
            return _context.Set<country>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalCountry> GetAllByPredicate(Expression<Func<DalCountry, bool>> f)
        {
            var criteria = ConvertExpression<DalCountry, country>.Convert(f, "OrmEntity");
            return _context.Set<country>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalCountry GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalCountry GetFirstByPredicate(Expression<Func<DalCountry, bool>> f)
        {
            var criteria = ConvertExpression<DalCountry, country>.Convert(f, "OrmEntity");
            var response = _context.Set<country>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalCountry e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<country>().Single(en => en.id_country == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
