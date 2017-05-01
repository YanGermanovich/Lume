using CustomExpressionVisitor;
using BLL.BllToDalMappers;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Services_Interface;
using BLL.Entities;

namespace DAL.Concrete
{
    public class PrizeTypeService : IService<BllPrizeType>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalPrizeType> _repository;

        public PrizeTypeService(IUnitOfWork uow, IRepository<DalPrizeType> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllPrizeType> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllPrizeType GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllPrizeType GetFirstByPredicate(Expression<Func<BllPrizeType, bool>> f)
        {
            var criteria = ConvertExpression<BllPrizeType, DalPrizeType>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllPrizeType> GetAllByPredicate(Expression<Func<BllPrizeType, bool>> f)
        {
            var criteria = ConvertExpression<BllPrizeType, DalPrizeType>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllPrizeType e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllPrizeType e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllPrizeType e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
