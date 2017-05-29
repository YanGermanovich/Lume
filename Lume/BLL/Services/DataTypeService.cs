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
    public class DataTypeService :IService<BllDataType>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalDataType> _repository;

        public DataTypeService(IUnitOfWork uow, IRepository<DalDataType> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllDataType> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllDataType GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllDataType GetFirstByPredicate(Expression<Func<BllDataType, bool>> f)
        {
            var criteria = ConvertExpression<BllDataType, DalDataType>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllDataType> GetAllByPredicate(Expression<Func<BllDataType, bool>> f)
        {
            var criteria = ConvertExpression<BllDataType, DalDataType>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllDataType e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllDataType e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllDataType e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
