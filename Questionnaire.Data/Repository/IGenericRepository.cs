using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Questionnaire.Data.Repository
{
    /// <summary>
    /// The Implementation of GenericRepository pattern.
    /// </summary>
    /// <Revisions>
    /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
    /// </Revisions>
    public interface IGenericRepository : IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether Lazy Loading is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Lazy Loading is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        bool LazyLoadingEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Automatic detection of changes is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Automatic detection of changes is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        bool AutoDetectChangesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Creation of Proxies is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Creation of Proxies is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// Sets the Context to provided value, or re-initializes it if none is provided.
        /// </summary>
        /// <param name="repo">
        /// The instance of <see cref="GenericRepository"/> class, that uses context that should be re-used.
        /// </param>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void ReinitializeContext(IGenericRepository repo = null);

        /// <summary>
        /// Finds the object within current Context using it's primary key.
        /// </summary>
        /// <param name="id">
        /// The value of the primary key.
        /// </param>
        /// <param name="withNoTracking">
        /// If true retrieved object won't be tracked by the context.
        /// </param>
        /// <param name="collectionsToInclude">
        /// Expressions defining collections to be loaded eagerly.
        /// </param>
        /// <typeparam name="T">
        /// The type of requested object.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of primary key.
        /// </typeparam>
        /// <returns>
        /// The object with specified primary key, or NULL if such object doesn't exist in context.
        /// </returns>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        T Find<T, TKey>(
            TKey id,
            bool withNoTracking = false,
            params Expression<Func<T, object>>[] collectionsToInclude)
            where T : class, new();

        /// <summary>
        /// Retrieves the collection of objects that comply with given condition.
        /// </summary>
        /// <param name="predicate">
        /// The condition for retrieving the objects.
        /// </param>
        /// <param name="withNoTracking">
        /// If true retrieved object won't be tracked by the context.
        /// </param>
        /// <param name="includingExpressions">
        /// Array of expressions defining related properties that should be included.
        /// </param>
        /// <typeparam name="T">
        /// The type of objects to be retrieved.
        /// </typeparam>
        /// <returns>
        /// The collection of objects that comply with given condition.
        /// </returns>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        IEnumerable<T> Get<T>(
            Expression<Func<T, bool>> predicate = null,
            bool withNoTracking = false,
            params Expression<Func<T, object>>[] includingExpressions)
            where T : class, new();

        /// <summary>
        /// Creates new instance of specified type.
        /// </summary>
        /// <param name="properties">
        /// An object containing properties that should be copied into newly created entity.
        /// </param>
        /// <param name="attachToContext">
        /// A Flag indicating whether newly created object should be attached to context.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to be created.
        /// </typeparam>
        /// <returns>
        /// New instance of specified type.
        /// </returns>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        T Create<T>(object properties = null, bool attachToContext = false) where T : class, new();

        /// <summary>
        /// Adds new object to the context.
        /// </summary>
        /// <param name="entity">
        /// Object to be added.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to be added.
        /// </typeparam>
        /// <Exceptions>
        /// ArgumentNullException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void Add<T>(T entity) where T : class, new();

        /// <summary>
        /// Updates object that is already being tracked by the context.
        /// Entity will be detached from current context and re-attached.
        /// This is required in order to enforce optimistic concurrency check.
        /// </summary>
        /// <param name="entity">
        /// Object to be updated.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to be updated.
        /// </typeparam>
        /// <Exceptions>
        /// InvalidOperationException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void Update<T>(T entity) where T : class, new();

        /// <summary>
        /// Adds object to context or inserts it if it already exists.
        /// </summary>
        /// <param name="entity">
        /// Object to be added or updated.
        /// </param>
        /// <param name="crossContext">
        /// Allows objects originating in other contexts, or detached objects to be added or inserted.
        /// </param>
        /// <param name="primaryKeyFunc">
        /// Function for determining value of primary key.
        /// If none is specified default one will be used.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to be added or updated.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of primary key.
        /// </typeparam>
        /// <Exceptions>
        /// InvalidOperationException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void AddOrUpdate<T, TKey>(
            T entity,
            bool crossContext = false,
            Func<T, TKey> primaryKeyFunc = null)
            where T : class, new();

        /// <summary>
        /// Deletes the object from the current context.
        /// </summary>
        /// <param name="entity">
        /// Object to be added or deleted.
        /// </param>
        /// <param name="primaryKeyFunc">
        /// Function for determining value of primary key.
        /// If none is specified default one will be used.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to be deleted.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of primary key.
        /// </typeparam>
        /// <Exceptions>
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void Delete<T, TKey>(
            T entity,
            Func<T, TKey> primaryKeyFunc = null)
            where T : class, new();

        /// <summary>
        /// Deletes the object from the current context.
        /// </summary>
        /// <param name="primaryKey">
        /// The primary key used to match the object to be deleted.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to be deleted.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of primary key.
        /// </typeparam>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        void Delete<T, TKey>(TKey primaryKey) where T : class, new();

        /// <summary>
        /// Saves changes to current context.
        /// </summary>
        /// <returns>
        /// Number of affected entities.
        /// </returns>
        /// <Exceptions>
        /// CustomException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        int Save();
    }
}
