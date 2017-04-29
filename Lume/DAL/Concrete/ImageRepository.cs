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
    public class ImageRepository : IRepository<DalImage>
    {
        private readonly DbContext _context;

        public ImageRepository(DbContext uow)
        {
            _context = uow;
        }

        public void Create(DalImage e)
        {
            var entity = e.ToOrm();
            _context.Set<image>().Add(entity);
        }

        public void Delete(DalImage e)
        {
            var entity = _context.Set<image>().Single(en => en.id_image == e.Id);
            _context.Set<image>().Remove(entity);
        }

        public IEnumerable<DalImage> GetAll()
        {
            return _context.Set<image>().ToList().Select(e => e.ToDal());
        }

        public IEnumerable<DalImage> GetAllByPredicate(Expression<Func<DalImage, bool>> f)
        {
            var criteria = ConvertExpression<DalImage, image>.Convert(f, "OrmEntity");
            return _context.Set<image>().Where(criteria.Compile()).ToList().Select(entity => entity.ToDal());
        }

        public DalImage GetById(int key)
        {
            return GetFirstByPredicate(entity => entity.Id == key);
        }

        public DalImage GetFirstByPredicate(Expression<Func<DalImage, bool>> f)
        {
            var criteria = ConvertExpression<DalImage, image>.Convert(f, "OrmEntity");
            var response = _context.Set<image>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDal();
        }

        public void Update(DalImage e)
        {
            var entity = e.ToOrm();
            var entityToUpdate = _context.Set<image>().Single(en => en.id_image == e.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            }
        }
    }
}
