using FoodApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.interfaces
{
    public interface IRepository<Entity> where Entity : BaseEntity
    {
        void Add(Entity entity);
        void SaveInclude(Entity entity, params string[] properties);
        void SaveExclude(Entity entity, params string[] properties);
        void Delete(Entity entity);
        void HardDelete(Entity entity);
        IQueryable<Entity> GetAll();
        IQueryable<Entity> GetAllWithDeleted();
        IQueryable<Entity> Get(Expression<Func<Entity, bool>> predicate);
        Entity GetByID(int id);
        void SaveChanges();
        bool ExistEntity(int id);
        public bool Any(Expression<Func<Entity, bool>> ex);



    }
}
