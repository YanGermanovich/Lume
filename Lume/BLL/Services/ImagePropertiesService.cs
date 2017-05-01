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
    public class ImagePropertiesService : IService<BllImageProperties>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalImageProperties> _repository;

        public ImagePropertiesService(IUnitOfWork uow, IRepository<DalImageProperties> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllImageProperties> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllImageProperties GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllImageProperties GetFirstByPredicate(Expression<Func<BllImageProperties, bool>> f)
        {
            var criteria = ConvertExpression<BllImageProperties, DalImageProperties>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllImageProperties> GetAllByPredicate(Expression<Func<BllImageProperties, bool>> f)
        {
            var criteria = ConvertExpression<BllImageProperties, DalImageProperties>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllImageProperties e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllImageProperties e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllImageProperties e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
