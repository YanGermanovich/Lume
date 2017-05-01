using BLL.BllToBllMappers;
using BLL.Entities;
using BLL.Services_Interface;
using CustomExpressionVisitor;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CityService : IService<BllCity>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalCity> _repository;

        public CityService(IUnitOfWork uow, IRepository<DalCity> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllCity> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllCity GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllCity GetFirstByPredicate(Expression<Func<BllCity, bool>> f)
        {
            var criteria = ConvertExpression<BllCity, DalCity>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllCity> GetAllByPredicate(Expression<Func<BllCity, bool>> f)
        {
            var criteria = ConvertExpression<BllCity, DalCity>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllCity e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllCity e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllCity e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
