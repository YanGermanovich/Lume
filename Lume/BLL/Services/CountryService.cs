using BLL.BllToDalMappers;
using BLL.Entities;
using BLL.Services_Interface;
using CustomExpressionVisitor;
using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bll.Services
{
    public class CountryService :IService<BllCountry>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalCountry> _repository;

        public CountryService(IUnitOfWork uow, IRepository<DalCountry> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllCountry> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllCountry GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllCountry GetFirstByPredicate(Expression<Func<BllCountry, bool>> f)
        {
            var criteria = ConvertExpression<BllCountry, DalCountry>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllCountry> GetAllByPredicate(Expression<Func<BllCountry, bool>> f)
        {
            var criteria = ConvertExpression<BllCountry, DalCountry>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllCountry e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllCountry e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllCountry e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
