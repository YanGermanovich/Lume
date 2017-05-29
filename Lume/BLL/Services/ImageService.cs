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
    public class ImageService : IService<BllImage>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalImage> _repository;

        public ImageService(IUnitOfWork uow, IRepository<DalImage> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllImage> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllImage GetEntitieById(long? key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllImage GetFirstByPredicate(Expression<Func<BllImage, bool>> f)
        {
            var criteria = ConvertExpression<BllImage, DalImage>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllImage> GetAllByPredicate(Expression<Func<BllImage, bool>> f)
        {
            var criteria = ConvertExpression<BllImage, DalImage>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllImage e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllImage e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllImage e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
