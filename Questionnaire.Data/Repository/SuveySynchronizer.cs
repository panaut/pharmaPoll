//using Questionnaire.Data.Model;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Questionnaire.Data.Repository
//{
//    public static class SuveySynchronizer
//    {
//        public static void Synchronize(this DbContext context, Survey survey)
//        {

//        }

//        private static void SyncEntity<T>(
//            DbContext context,
//            T currentEntity)
//            where T : class
//        {
//            DbSet<T> entitySet = context.Set<T>();
//            var key = PrimaryKey(currentEntity);

//            if (key == 0)
//            {
//                // Simply add entity
//                context.Set<T>().Add(currentEntity);
//            }
//            else
//            {
//                var dbEntity = entitySet.Find(key);

//                if (dbEntity == null)
//                {
//                    // insert is required
//                    context.Set<T>().Add(currentEntity);
//                }
//                else
//                {
//                    var entry = context.Entry(dbEntity);
//                    if (entry.State == EntityState.Added)
//                    {
//                        entry.CurrentValues.SetValues(currentEntity);
//                    }
//                }
//            }
//        }

//        private static void SyncOwnedEntity<T, C>(
//            DbContext context,
//            T currentEntityOwner,
//            Expression<Func<T, C>> expEntity)
//            where T : class
//            where C : class
//        {
//            var propertyGetter = expEntity.Compile();
//            var currentOwnedEntity = propertyGetter.Invoke(currentEntityOwner);

//            if (currentOwnedEntity != null)
//            {
//                SyncEntity(context, currentOwnedEntity);
//            }
//            else
//            {
//                // This one should be deleted
//                DbSet<T> dbSet = context.Set<T>();
//                var dbEntityOwner = dbSet.Find(PrimaryKey(currentEntityOwner));
//                var dbOwnedEntity = propertyGetter.Invoke(dbEntityOwner);

//                if (dbOwnedEntity != null)
//                {
//                    // Delete the property
//                    context.Entry(dbOwnedEntity).State = EntityState.Deleted;
//                }
//            }
//        }

//        private static void SyncOwnedCollection<T, C>(
//            DbContext context,
//            T currentCollectionOwner,
//            Expression<Func<T, IEnumerable<C>>> expCollection)
//            where T : class
//            where C : class
//        {
//            // Get the original collection owner
//            DbSet<T> entitySet = context.Set<T>();
//            var dbCollectionOwner = entitySet.Find(PrimaryKey(currentCollectionOwner));
//            if (dbCollectionOwner == nul)


//                var propertyGetter = expCollection.Compile();

//            // Get Current Collection
//            var currentCollection = propertyGetter.Invoke(currentCollectionOwner);

//            if (currentCollection == null)
//            {
//                // Delete all elements from the original collection
//            }

//            // Get original Collection



//            if (dbCollectionOwner == null)
//                throw new ArgumentException("Collection Owner should be synchronized first.");

//            var originalCollection = propertyGetter.Invoke(currentCollectionOwner);
//        }

//        #region Retrieve Primary Key

//        private static object PrimaryKey<T>(T entity, Expression<Func<T, object>> expId)
//        {
//            var funcId = expId.Compile();
//            return funcId.Invoke(entity);
//        }

//        private static object PrimaryKey<T>(T entity, Func<T, object> expId)
//        {
//            return expId.Invoke(entity);
//        }

//        // We Assume that primary key is ID
//        private static int PrimaryKey<T>(T entity)
//        {
//            var propId = typeof(T).GetProperty("Id");

//            return (int)propId.GetValue(entity);
//        }

//        #endregion
//    }
//}
