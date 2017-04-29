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
    public class PrizeTypeRepository : IRepository<DalPrizeType>
    {
        private readonly DbContext _context;

        public PrizeTypeRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalPrizeType e)
        {
            var entity = e.ToOrm();
            _context.Set<type_of_prize>().Add(entity);
        }

        public void Delete(DalPrizeType e)
        {
            var entity = _context.Set<type_of_prize>().Single(en => en.id_type == e.Id);
            _context.Set<type_of_prize>().Remove(entity);
        }

        public IEnumerable<DalPrizeType> GetAll()
        {
            return _context.Set<type_of_prize>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalPrizeType> GetAllByPredicate(Expression<Func<DalPrizeType, bool>> f)
        {
            var criteria = ConvertExpression<DalPrizeType, type_of_prize>.Convert(f, "OrmEntity");
            return _context.Set<type_of_prize>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalPrizeType GetById(int key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalPrizeType GetFirstByPredicate(Expression<Func<DalPrizeType, bool>> f)
        {
            var criteria = ConvertExpression<DalPrizeType, type_of_prize>.Convert(f, "OrmEntity");
            var response = _context.Set<type_of_prize>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalPrizeType e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<type_of_prize>().Single(en => en.id_type == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
