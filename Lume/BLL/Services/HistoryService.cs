using CustomExpressionVisitor;
using BLL.BllToDalMappers;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Entities;
using BLL.Services_Interface;

namespace Bll.Services
{
    public class HistoryService : IService<BllHistory>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalHistory> _repository;

        public HistoryService(IUnitOfWork uow, IRepository<DalHistory> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllHistory> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllHistory GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllHistory GetFirstByPredicate(Expression<Func<BllHistory, bool>> f)
        {
            var criteria = ConvertExpression<BllHistory, DalHistory>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllHistory> GetAllByPredicate(Expression<Func<BllHistory, bool>> f)
        {
            var criteria = ConvertExpression<BllHistory, DalHistory>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllHistory e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllHistory e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllHistory e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
