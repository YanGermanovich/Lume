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

namespace DAL.Concrete
{
    public class UserService :IService<BllUser>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalUser> _repository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public IEnumerable<BllUser> GetAllEntities()
        {
            return _repository.GetAll().Select(e => e.ToBll());
        }

        public BllUser GetEntitieById(int key)
        {
            var response = _repository.GetById(key);
            return response == null ? null : response.ToBll();
        }

        public BllUser GetFirstByPredicate(Expression<Func<BllUser, bool>> f)
        {
            var criteria = ConvertExpression<BllUser, DalUser>.Convert(f, "DalEntity");
            var response = _repository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBll();
        }

        public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> f)
        {
            var criteria = ConvertExpression<BllUser, DalUser>.Convert(f, "DalEntity");
            return _repository.GetAllByPredicate(criteria).Select(e => e.ToBll());
        }

        public void Create(BllUser e)
        {
            _repository.Create(e.ToDal());
            _uow.Commit();
        }

        public void Delete(BllUser e)
        {
            _repository.Delete(e.ToDal());
            _uow.Commit();
        }

        public void Update(BllUser e)
        {
            _repository.Update(e.ToDal());
            _uow.Commit();
        }
    }
}
