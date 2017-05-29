using CustomExpressionVisitor;
using DAL.DalToOrmMappers;
using DAL.DTO;
using DAL.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class DataTypeRepository :IRepository<DalDataType>
    {
        private readonly DbContext _context;

        public DataTypeRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalDataType e)
        {
            var entity = e.ToOrm();
            _context.Set<type_of_data>().Add(entity);
        }

        public void Delete(DalDataType e)
        {
            var entity = _context.Set<type_of_data>().Single(en => en.id_Type == e.Id);
            _context.Set<type_of_data>().Remove(entity);
        }

        public IEnumerable<DalDataType> GetAll()
        {
            return _context.Set<type_of_data>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalDataType> GetAllByPredicate(Expression<Func<DalDataType, bool>> f)
        {
            var criteria = ConvertExpression<DalDataType, type_of_data>.Convert(f, "OrmEntity");
            return _context.Set<type_of_data>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalDataType GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalDataType GetFirstByPredicate(Expression<Func<DalDataType, bool>> f)
        {
            var criteria = ConvertExpression<DalDataType, type_of_data>.Convert(f, "OrmEntity");
            var response = _context.Set<type_of_data>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalDataType e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<type_of_data>().Single(en => en.id_Type == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
