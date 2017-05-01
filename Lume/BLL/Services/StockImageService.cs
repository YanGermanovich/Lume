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
    public class StockImageService : IService<BllStockImage>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalStockImage> _repository;

        public StockImageService(IUnitOfWork uow, IRepository<DalStockImage> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllStockImage> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllStockImage GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllStockImage GetFirstByPredicate(Expression<Func<BllStockImage, bool>> f)
        {
            var criteria = ConvertExpression<BllStockImage, DalStockImage>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllStockImage> GetAllByPredicate(Expression<Func<BllStockImage, bool>> f)
        {
            var criteria = ConvertExpression<BllStockImage, DalStockImage>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllStockImage e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllStockImage e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllStockImage e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
