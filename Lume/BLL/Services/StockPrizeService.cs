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
    public class StockPrizeService : IService<BllStockPrize>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalStockPrize> _repository;

        public StockPrizeService(IUnitOfWork uow, IRepository<DalStockPrize> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllStockPrize> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllStockPrize GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllStockPrize GetFirstByPredicate(Expression<Func<BllStockPrize, bool>> f)
        {
            var criteria = ConvertExpression<BllStockPrize, DalStockPrize>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllStockPrize> GetAllByPredicate(Expression<Func<BllStockPrize, bool>> f)
        {
            var criteria = ConvertExpression<BllStockPrize, DalStockPrize>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllStockPrize e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllStockPrize e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllStockPrize e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
