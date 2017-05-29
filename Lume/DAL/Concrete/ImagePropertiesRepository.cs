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
    public class ImagePropertiesRepository : IRepository<DalImageProperties>
    {
        private readonly DbContext _context;

        public ImagePropertiesRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalImageProperties e)
        {
            var entity = e.ToOrm();
            _context.Set<properties_images>().Add(entity);
        }

        public void Delete(DalImageProperties e)
        {
            var entity = _context.Set<properties_images>().Single(en => en.id_propertie == e.Id);
            _context.Set<properties_images>().Remove(entity);
        }

        public IEnumerable<DalImageProperties> GetAll()
        {
            return _context.Set<properties_images>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalImageProperties> GetAllByPredicate(Expression<Func<DalImageProperties, bool>> f)
        {
            var criteria = ConvertExpression<DalImageProperties, properties_images>.Convert(f, "OrmEntity");
            return _context.Set<properties_images>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalImageProperties GetById(long? key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalImageProperties GetFirstByPredicate(Expression<Func<DalImageProperties, bool>> f)
        {
            var criteria = ConvertExpression<DalImageProperties, properties_images>.Convert(f, "OrmEntity");
            var response = _context.Set<properties_images>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalImageProperties e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<properties_images>().Single(en => en.id_propertie == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
