using CustomExpressionVisitor;
using BLL.BllToDalMappers;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Services_Interface;
using BLL.Entities;

namespace Bll.Services
{
    public class StockTypeService : IService<BllStockType>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalStockType> _repository;

        public StockTypeService(IUnitOfWork uow, IRepository<DalStockType> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllStockType> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllStockType GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllStockType GetFirstByPredicate(Expression<Func<BllStockType, bool>> f)
        {
            var criteria = ConvertExpression<BllStockType, DalStockType>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllStockType> GetAllByPredicate(Expression<Func<BllStockType, bool>> f)
        {
            var criteria = ConvertExpression<BllStockType, DalStockType>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllStockType e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllStockType e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllStockType e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
