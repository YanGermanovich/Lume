using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.BllToDalMappers;
using CustomExpressionVisitor;
using BLL.Entities;
using BLL.Services_Interface;

namespace DAL.Concrete
{
    public class AvatarService : IService<BllAvatar>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalAvatar> _repository;

        public AvatarService(IUnitOfWork uow, IRepository<DalAvatar> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllAvatar> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllAvatar GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllAvatar GetFirstByPredicate(Expression<Func<BllAvatar, bool>> f)
        {
            var criteria = ConvertExpression<BllAvatar, DalAvatar>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllAvatar> GetAllByPredicate(Expression<Func<BllAvatar, bool>> f)
        {
            var criteria = ConvertExpression<BllAvatar, DalAvatar>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllAvatar e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllAvatar e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllAvatar e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
