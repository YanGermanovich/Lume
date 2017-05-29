using BLL.Entities;
using BLL.Services_Interface;
using CustomExpressionVisitor;
using BLL.BllToDalMappers;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class EventService : IService<BllEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalEvent> _repository;

        public EventService(IUnitOfWork uow, IRepository<DalEvent> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllEvent> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllEvent GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllEvent GetFirstByPredicate(Expression<Func<BllEvent, bool>> f)
        {
            var criteria = ConvertExpression<BllEvent, DalEvent>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllEvent> GetAllByPredicate(Expression<Func<BllEvent, bool>> f)
        {
            var criteria = ConvertExpression<BllEvent, DalEvent>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllEvent e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllEvent e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllEvent e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
