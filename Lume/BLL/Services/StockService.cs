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

namespace Bll.Services
{
    public class StockService : IService<BllStock>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalStock> _repository;

        public StockService(IUnitOfWork uow, IRepository<DalStock> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllStock> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllStock GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllStock GetFirstByPredicate(Expression<Func<BllStock, bool>> f)
        {
            var criteria = ConvertExpression<BllStock, DalStock>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllStock> GetAllByPredicate(Expression<Func<BllStock, bool>> f)
        {
            var criteria = ConvertExpression<BllStock, DalStock>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllStock e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllStock e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllStock e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
