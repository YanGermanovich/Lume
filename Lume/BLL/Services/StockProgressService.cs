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
using BLL.Entities;
using BLL.Services_Interface;

namespace DAL.Concrete
{
    public class StockProgressService :IService<BllStockProgress>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalStockProgress> _repository;

        public StockProgressService(IUnitOfWork uow, IRepository<DalStockProgress> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllStockProgress> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllStockProgress GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllStockProgress GetFirstByPredicate(Expression<Func<BllStockProgress, bool>> f)
        {
            var criteria = ConvertExpression<BllStockProgress, DalStockProgress>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllStockProgress> GetAllByPredicate(Expression<Func<BllStockProgress, bool>> f)
        {
            var criteria = ConvertExpression<BllStockProgress, DalStockProgress>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllStockProgress e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllStockProgress e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllStockProgress e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
