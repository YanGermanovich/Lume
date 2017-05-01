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

namespace DAL.Concrete
{
    public class PrizeService : IService<BllPrize>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalPrize> _repository;

        public PrizeService(IUnitOfWork uow, IRepository<DalPrize> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllPrize> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllPrize GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllPrize GetFirstByPredicate(Expression<Func<BllPrize, bool>> f)
        {
            var criteria = ConvertExpression<BllPrize, DalPrize>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllPrize> GetAllByPredicate(Expression<Func<BllPrize, bool>> f)
        {
            var criteria = ConvertExpression<BllPrize, DalPrize>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllPrize e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllPrize e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllPrize e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
