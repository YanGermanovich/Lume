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
    public class EventRepository : IRepository<DalEvent>
    {
        private readonly DbContext _context;

        public EventRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalEvent e)
        {
            var entity = e.ToOrm();
            _context.Set<@event>().Add(entity);
        }

        public void Delete(DalEvent e)
        {
            var entity = _context.Set<@event>().Single(en => en.id_event== e.Id);
            _context.Set<@event>().Remove(entity);
        }

        public IEnumerable<DalEvent> GetAll()
        {
            return _context.Set<@event>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalEvent> GetAllByPredicate(Expression<Func<DalEvent, bool>> f)
        {
            var criteria = ConvertExpression<DalEvent, @event>.Convert(f, "OrmEntity");
            return _context.Set<@event>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalEvent GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalEvent GetFirstByPredicate(Expression<Func<DalEvent, bool>> f)
        {
            var criteria = ConvertExpression<DalEvent, @event>.Convert(f, "OrmEntity");
            var response = _context.Set<@event>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalEvent e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<@event>().Single(en => en.id_event == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
