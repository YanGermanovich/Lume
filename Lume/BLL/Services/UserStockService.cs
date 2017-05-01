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
    public class UserStockService : IService<BllUserStock>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalUserStock> _repository;

        public UserStockService(IUnitOfWork uow, IRepository<DalUserStock> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllUserStock> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllUserStock GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllUserStock GetFirstByPredicate(Expression<Func<BllUserStock, bool>> f)
        {
            var criteria = ConvertExpression<BllUserStock, DalUserStock>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllUserStock> GetAllByPredicate(Expression<Func<BllUserStock, bool>> f)
        {
            var criteria = ConvertExpression<BllUserStock, DalUserStock>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllUserStock e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllUserStock e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllUserStock e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
