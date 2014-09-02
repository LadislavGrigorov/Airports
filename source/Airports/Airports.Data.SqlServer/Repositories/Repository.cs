namespace Airports.Data.SqlServer.Repositories
{
    using Airports.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private IAirportsDbContextSqlServer dbContext;
        private IDbSet<T> dataSet;

        public Repository(IAirportsDbContextSqlServer dbContext)
        {
            this.dbContext = dbContext;
            this.dataSet = this.dbContext.GetDataSet<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.dataSet.AsQueryable();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.GetAll().Where(conditions);
        }

        public virtual void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dataSet.Attach(entity);
            }

            return entry;
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException("No method implementation in parent class. Use child classes instead.");
        }
    }
}
