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
    public class HistoryRepository : IRepository<DalHistory>
    {
        private readonly DbContext _context;

        public HistoryRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalHistory e)
        {
            var entity = e.ToOrm();
            _context.Set<history>().Add(entity);
        }

        public void Delete(DalHistory e)
        {
            var entity = _context.Set<history>().Single(en => en.id_History== e.Id);
            _context.Set<history>().Remove(entity);
        }

        public IEnumerable<DalHistory> GetAll()
        {
            return _context.Set<history>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalHistory> GetAllByPredicate(Expression<Func<DalHistory, bool>> f)
        {
            var criteria = ConvertExpression<DalHistory, history>.Convert(f, "OrmEntity");
            return _context.Set<history>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalHistory GetById(int key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalHistory GetFirstByPredicate(Expression<Func<DalHistory, bool>> f)
        {
            var criteria = ConvertExpression<DalHistory, history>.Convert(f, "OrmEntity");
            var response = _context.Set<history>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalHistory e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<history>().Single(en => en.id_History== e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
