
using FoodApp.Core.Entities;
using FoodApp.Core.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Repository.Data.Repositorys
{
    public class Repository<Entity> : IRepository<Entity> where Entity : BaseEntity
    {
        Context _context;
        DbSet<Entity> _dbSet;

        // entity.CreatedDate = DateTime.Now;
        //entity.CreatedBy = userID;
        public Repository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public void Add(Entity entity)=> _dbSet.Add(entity);

        public void SaveInclude(Entity entity, params string[] properties)
        {
            var local = _dbSet.Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entry = null;

            if (local is null)
            {
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.ChangeTracker.Entries<Entity>()
                    .FirstOrDefault(x => x.Entity.ID == entity.ID);
            }
            if (entry == null)
            {
                throw new InvalidOperationException($"Entity with ID {entity.ID} is not being tracked.");
            }

            foreach (var property in entry.Properties)
            {
                if (properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
        }

        public void SaveExclude(Entity entity, params string[] properties)
        {
            var local = _dbSet.Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entry = null;

            if (local is null)
            {
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.ChangeTracker.Entries<Entity>()
                    .FirstOrDefault(x => x.Entity.ID == entity.ID);
            }

            if (entry == null)
            {
                throw new InvalidOperationException($"Entity with ID {entity.ID} is not being tracked.");
            }
            foreach (var property in entry.Properties)
            {
                if (!properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
        }


        public void Delete(Entity entity)
        {
            entity.Deleted = true;
            SaveInclude(entity, nameof(BaseEntity.Deleted));
        }

        public void HardDelete(Entity entity)=>_dbSet.Remove(entity);
        public IQueryable<Entity> Get(Expression<Func<Entity, bool>> predicate)=> GetAll().Where(predicate);
        public bool Any(Expression<Func<Entity, bool>> expression) => _dbSet.Any(expression);
        public bool ExistEntity(int id) => _dbSet.Any(e => e.ID == id && !e.Deleted); 
        public IQueryable<Entity> GetAll()=> _dbSet.Where(x => !x.Deleted);
        public IQueryable<Entity> GetAllWithDeleted()=> _dbSet;
        public Entity GetByID(int id)=> Get(x => x.ID == id).FirstOrDefault();
        public void SaveChanges()=>  _context.SaveChanges();

    
    }
}
