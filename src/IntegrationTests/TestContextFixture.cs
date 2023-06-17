namespace ContosoUniversity.IntegrationTests
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Mime;
    using DAL;
    using DependencyResolution;
    using MediatR;

    public class TestContextFixture
    {
        private readonly StructureMapDependencyScope scope;

        public TestContextFixture(StructureMapDependencyScope scope)
        {
            this.scope = scope;
        }

        public void SetUp()
        {
            scope.CreateNestedContainer();
        }

        public void SaveAll(params object[] entities)
        {
            Do(dbContext =>
            {
                foreach (var entity in entities)
                {
                    var entry = dbContext.ChangeTracker.Entries().FirstOrDefault(entityEntry => entityEntry.Entity == entity);
                    if (entry == null)
                    {
                        dbContext.Set(entity.GetType()).Add(entity);
                    }
                }
            });
        }

        public void Reload<TEntity, TIdentity>(
            ref TEntity entity,
            TIdentity id)
            where TEntity : class
        {
            TEntity e = entity;

            Do(ctx => e = ctx.Set<TEntity>().Find(id));

            entity = e;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Do(dbContext => dbContext.Set<TEntity>().Remove(entity));
        }

        public void Do(Action action)
        {
            var dbContext = scope.GetInstance<SchoolContext>();
            try
            {
                dbContext.BeginTransaction();
                action();
                dbContext.CloseTransaction();
            }
            catch (Exception e)
            {
                dbContext.CloseTransaction(e);
                throw;
            }
        }

        public void Do(Action<DbContext> action)
        {
            var dbContext = scope.GetInstance<SchoolContext>();
            try
            {
                dbContext.BeginTransaction();
                action(dbContext);
                dbContext.CloseTransaction();
            }
            catch (Exception e)
            {
                dbContext.CloseTransaction(e);
                throw;
            }
        }

        public void DoClean(Action<DbContext> action)
        {
            var connString = LocalDbFactory.Instance.CreateConnectionStringBuilder();
            connString.InitialCatalog = "ContosoUniversity";
            var dbContext = new SchoolContext(connString.ToString());

            try
            {
                dbContext.BeginTransaction();
                action(dbContext);
                dbContext.CloseTransaction();
            }
            catch (Exception e)
            {
                dbContext.CloseTransaction(e);
                throw;
            }
        }

        public void Send(IRequest message)
        {
            Send((IRequest<Unit>)message);
        }

        public TResult Send<TResult>(IRequest<TResult> message, bool asNonPullStep = false)
        {
            var result = default(TResult);

            var context = scope.GetInstance<SchoolContext>();

            context.BeginTransaction();

            var mediator = scope.GetInstance<IMediator>();

            Exception exc = null;
            try
            {
                result = mediator.Send(message);
            }
            catch (Exception e)
            {
                exc = e;
            }

            context.CloseTransaction(exc);

            if (exc != null)
            {
                throw new Exception("Failed to send message.", exc);
            }

            return result;
        }
    }
}