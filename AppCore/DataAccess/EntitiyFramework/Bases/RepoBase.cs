using System.Linq.Expressions;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;

namespace AppCore.DataAccess.EntitiyFramework.Bases
{
    public abstract class RepoBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        protected readonly DbContext _dbContext;

        protected RepoBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // List<Urun> urunler = _repoBase.Query().ToList();
        // List<Urun> urunler = _repoBase.Query(urun => urun.Kategori).ToList();
        public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] entitiesToInclude) // Read -> C(R)UD
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }
            return query;
        }

        // List<Urun> urunler = _repoBase.Query( urun => urun.Adi.Contains == ("ASUS")).ToList();
        // List<Urun> urunler = _repoBase.Query(urun => urun.StokMiktari >= 10 && urun.StokMiktari <= 20, urun => urun.Kategori);
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] entitiesToInclude)
        {
            return Query(entitiesToInclude).Where(predicate);
        }

        public virtual IQueryable<TRelationalEntity> Query<TRelationalEntity>() where TRelationalEntity : class, new()
        {
            return _dbContext.Set<TRelationalEntity>().AsQueryable();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Any(predicate);
        }

        // -ADD-
        // Urun urun = new Urun {Adi = "ASUS" ...}
        // repoBase.Add(urun);
        // List<Urun> urunler = new List<Urun>()
        // {
        //  new urun = {Adi = "ASUS", ...},
        //  new urun = {Adi = "HP", ...}
        // };
        //   foreach(Urun urun in urunler)
        // {
        //     _repoBase.Add(urun, false); 
        // }
        // _repoBase.Save();
        public virtual void Add(TEntity entity, bool save = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }

        // -UPDATE-
        // Urun urun = new Urun { Id = 1, Adi = "ASUS" ...}
        // repoBase.Update(urun);
        // List<Urun> urunler = new List<Urun>()
        // {
        //  new urun() { Id = 1, Adi = "ASUS", ...},
        //  new urun() { Id = 2, Adi = "HP", ...}
        // };
        //   foreach(Urun urun in urunler)
        // {
        //     _repoBase.Update(urun, false); 
        // }
        // _repoBase.Save();
        public virtual void Update(TEntity entity, bool save = true)
        {
            entity.Guid = Guid.NewGuid().ToString();
            _dbContext.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        // -DELETE-
        // Urun urun = _repoBase.Query().SingleOrDefault( u => u.Id == 1);
        // Urun urun = _repoBase.Query().Where(u => u.Id == 1).SingleOrDefault();
        // Urun urun = _repoBase.Query(u => u.Id == 1).SingleOrDefault();
        public virtual void Delete(TEntity entity, bool save = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (save)
                Save();
        }

        public virtual void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false)where TRelationalEntity : class, new()
        {
            var relationalEntities =  Query<TRelationalEntity>().Where(predicate).ToList();
            _dbContext.Set<TRelationalEntity>().RemoveRange(relationalEntities);
            if (save)
                Save();
        }

        // _repoBase.Delete(1);
        public virtual void Delete(int id, bool save = true)
        {
            // var entity = _dbContext.Set<TEntity>().Find(id);
            var entity = _dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
            Delete(entity, save);
        }

        // _repoBase.Delete(u => u.Id == 1);
        // _repoBase.Delete(u => u.ExpirationDate < Datetime.Now);

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = Query(predicate).ToList();
            foreach (var entity in entities)
            {
                Delete(entity, false);
            }
            if (save)
                Save();
        }

        public virtual int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                // exc.Message
                // exc üzerinden loglama
                throw exc;
            }
        }

        public void Dispose() // işlem sonucu null ise çöpe atılır
        {
            _dbContext?.Dispose(); // ? unutursa null referance exeption alırsın
            GC.SuppressFinalize(this);
        }
    }
}
