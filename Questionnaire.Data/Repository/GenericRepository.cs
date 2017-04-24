using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Questionnaire.Data.Repository
{
    /// <summary>
    /// The Implementation of GenericRepository pattern.
    /// </summary>
    /// <Revisions>
    /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
    /// </Revisions>
    public class GenericRepository : IGenericRepository, IDisposable
    {
        /// <summary>
        /// The Database Context Object.
        /// </summary>
        private DbContext myContext;

        /// <summary>
        /// The instance of Context Factory object.
        /// It is used to generate instances of Database Context.
        /// </summary>
        private IContextFactory myContextFactory;

        /// <summary>
        /// Flag that indicates whether Lazy Loading is active.
        /// </summary>
        private bool lazyLoadingEnabled;

        /// <summary>
        /// Flag that indicates whether Automatic detection of changes is active.
        /// </summary>
        private bool autoDetectChangesEnabled;

        /// <summary>
        /// Flag that indicates whether Creation of Proxies is active.
        /// </summary>
        private bool proxyCreationEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository"/> class.
        /// </summary>
        /// <param name="contextFact">
        /// The context factory object.
        /// </param>
        /// <CallSequence>
        /// SetConfig
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public GenericRepository(
            IContextFactory contextFact)
        {
            this.myContextFactory = contextFact;
            this.SetConfig(true, true, true);
        }

        /// <summary>
        /// Gets or sets a value indicating whether Lazy Loading is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Lazy Loading is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public bool LazyLoadingEnabled
        {
            get
            {
                return this.lazyLoadingEnabled;
            }

            set
            {
                this.lazyLoadingEnabled = value;
                if (this.myContext != null)
                {
                    this.myContext.Configuration.LazyLoadingEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Automatic detection of changes is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Automatic detection of changes is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this.autoDetectChangesEnabled;
            }

            set
            {
                this.autoDetectChangesEnabled = value;
                if (this.myContext != null)
                {
                    this.myContext.Configuration.AutoDetectChangesEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Creation of Proxies is active for the current context.
        /// </summary>
        /// <value>
        /// Flag that indicates whether Creation of Proxies is active.
        /// </value>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public bool ProxyCreationEnabled
        {
            get
            {
                return this.proxyCreationEnabled;
            }

            set
            {
                this.proxyCreationEnabled = value;
                if (this.myContext != null)
                {
                    this.myContext.Configuration.ProxyCreationEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets the value of Database Context.
        /// </summary>
        /// <value>
        /// Database Context Object used by this Repository instance.
        /// </value>
        /// <CallSequence>
        /// GetNewContext
        /// SetConfig
        /// Dispose
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        protected DbContext Context
        {
            get
            {
                if (this.myContext == null)
                {
                    this.myContext = this.myContextFactory.GetNewContext();
                    this.SetConfig(this.LazyLoadingEnabled, this.AutoDetectChangesEnabled, this.ProxyCreationEnabled);
                }

                return this.myContext;
            }

            private set
            {
                this.Dispose();
                this.myContext = value;
                this.SetConfig(
                    value.Configuration.LazyLoadingEnabled,
                    value.Configuration.AutoDetectChangesEnabled,
                    value.Configuration.ProxyCreationEnabled);
            }
        }

        /// <summary>
        /// Sets the Context to provided value, or re-initializes it if none is provided.
        /// </summary>
        /// <param name="repo">
        /// The instance of <see cref="GenericRepository"/> class, that uses context that should be re-used.
        /// </param>
        /// <CallSequence>
        /// Dispose
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void ReinitializeContext(IGenericRepository repo = null)
        {
            // Cast parameter into generic repository
            var genRepo = repo as GenericRepository;
            if (genRepo != null)
            {
                this.Context = genRepo.Context;
            }
            else if (repo == null)
            {
                this.Dispose();
            }
        }

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
        /// <returns>The object with specified primary key, or NULL if such object doesn't exist in context.</returns>
        /// <CallSequence>
        /// Set
        /// Find
        /// Format
        /// nameof
        /// Entry
        /// Collection
        /// Load
        /// </CallSequence>
        /// <Exceptions>
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public T Find<T, TKey>(
            TKey id,
            bool withNoTracking = false,
            params Expression<Func<T, object>>[] collectionsToInclude)
            where T : class, new()
        {
            // Get a DbSet object
            var databaseSet = this.Context.Set<T>();

            // Find an entity
            var entity = databaseSet.Find(id);

            // We may nees an entry for this entity
            DbEntityEntry<T> entry = null;

            // load specified collections
            foreach (var inclExp in collectionsToInclude)
            {
                // Is provided function Member Expression?
                var memberExp = inclExp.Body as MemberExpression;

                if (memberExp == null)
                {
                    throw new ArgumentException(
                        string.Format("Provided expression ({0}) must be a MemberExpression", inclExp),
                        nameof(collectionsToInclude));
                }

                // Finally get the navigation property name
                var propName = memberExp.Member.Name;

                if (entry == null)
                {
                    // Get an Entry object
                    entry = this.Context.Entry<T>(entity);
                }

                // Load members of this collection
                entry.Collection(propName).Load();
            }

            if (entity != null && withNoTracking)
            {
                if (entry == null)
                {
                    // Get an Entry object
                    entry = this.Context.Entry<T>(entity);
                }

                // Set it's state to 'detached'
                entry.State = EntityState.Detached;
            }

            return entity;
        }

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
        /// <CallSequence>
        /// Set
        /// Include
        /// AsNoTracking
        /// Where
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public IEnumerable<T> Get<T>(
            Expression<Func<T, bool>> predicate = null,
            bool withNoTracking = false,
            params Expression<Func<T, object>>[] includingExpressions) where T : class, new()
        {
            IEnumerable<T> retVal;      // value to be returned.

            // Get a DbSet object
            IQueryable<T> databaseSet = this.Context.Set<T>();

            if (includingExpressions != null)
            {
                // Append each expression as Including Statement
                foreach (var inclExp in includingExpressions)
                {
                    databaseSet = databaseSet.Include(inclExp);
                }
            }

            if (withNoTracking)
            {
                databaseSet = databaseSet.AsNoTracking();
            }

            if (predicate != null)
            {
                retVal = databaseSet.Where(predicate);
            }
            else
            {
                retVal = databaseSet;
            }

            return retVal;
        }

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
        /// <CallSequence>
        /// Set
        /// Create
        /// Entry
        /// SetValues
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public T Create<T>(object properties = null, bool attachToContext = false) where T : class, new()
        {
            // Create new entity
            var entity = this.Context.Set<T>().Create<T>();

            if (properties != null)
            {
                // Get a DbEntry object
                var entry = this.Context.Entry<T>(entity);
                entry.State = EntityState.Added;
                entry.CurrentValues.SetValues(properties);
                if (!attachToContext)
                {
                    entry.State = EntityState.Detached;
                }

                entity = entry.Entity;
            }

            return entity;
        }

        /// <summary>
        /// Adds new object to the context.
        /// </summary>
        /// <param name="entity">
        /// Object to be added.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to be added.
        /// </typeparam>
        /// <CallSequence>
        /// nameof
        /// Set
        /// Add
        /// </CallSequence>
        /// <Exceptions>
        /// ArgumentNullException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void Add<T>(T entity) where T : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Context.Set<T>().Add(entity);
        }

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
        /// <CallSequence>
        /// nameof
        /// Entry
        /// Set
        /// Attach
        /// </CallSequence>
        /// <Exceptions>
        /// InvalidOperationException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void Update<T>(T entity) where T : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.Context.Entry<T>(entity);      // A DbEntry object from current context

            switch (entry.State)
            {
                case EntityState.Detached:
                    throw new InvalidOperationException("Provided entity is not being tracked by the current context.");
                case EntityState.Unchanged:
                    break;
                case EntityState.Added:
                    break;
                case EntityState.Deleted:
                    throw new InvalidOperationException("Provided entity has been deleted.");
                case EntityState.Modified:
                    break;
                default:
                    break;
            }

            // To make sure optimistic concurrency is taken into consideration,
            // this object should be re-attached to context            
            // Remove dbEntity from Context
            entry.State = EntityState.Detached;

            // Re-attach entity to context
            // (this is to force Optimistic Concurrency to use RowVersion stored in the field)
            this.Context.Set<T>().Attach(entry.Entity);
            entry = this.Context.Entry<T>(entry.Entity);

            // It's state after re-attaching will be 'Modified'.
            entry.State = EntityState.Modified;
        }

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
        /// <CallSequence>
        /// nameof
        /// Entry
        /// primaryKeyFunc
        /// Find
        /// DefaultPrimaryKeyFunc
        /// Invoke
        /// AttachExistingObjectToContext
        /// </CallSequence>
        /// <Exceptions>
        /// InvalidOperationException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void AddOrUpdate<T, TKey>(
            T entity,
            bool crossContext = false,
            Func<T, TKey> primaryKeyFunc = null)
            where T : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.Context.Entry<T>(entity);      // A DbEntry object from current context

            switch (entry.State)
            {
                case EntityState.Detached:
                    T databaseEntity = null;                // A mathcing object from the current context.
                    bool newEntity = false;                 // A flag indicating whether this object should be added.

                    if (primaryKeyFunc != null)
                    {
                        // Value of primary key
                        var primaryKey = primaryKeyFunc(entity);
                        if (primaryKey == null)
                        {
                            newEntity = true;
                        }
                        else if (crossContext)
                        {
                            databaseEntity = this.Find<T, TKey>(primaryKey);
                        }
                    }
                    else
                    {
                        // Value of primary key
                        var primaryKey = this.DefaultPrimaryKeyFunc<T>().Invoke(entity);
                        if (primaryKey == null)
                        {
                            newEntity = true;
                        }
                        else if (crossContext)
                        {
                            databaseEntity = this.Find<T, int>(primaryKey.Value);
                        }
                    }

                    if (newEntity)
                    {
                        // It's being added - ALLOW
                        entry.State = EntityState.Added;
                    }
                    else if (crossContext && databaseEntity != null)
                    {
                        // It's from another context, howerver matching entity has been found. ALLOW if CROSSCONTEXT
                        this.AttachExistingObjectToContext<T>(entity, databaseEntity);
                    }
                    else if (crossContext && databaseEntity == null)
                    {
                        // It's from another context, however matching entity wasn't found - STOP
                        throw new InvalidOperationException(
                            @"Provided Entity originates from another Context, 
                            but current context is unable to match primary key.");
                    }
                    else
                    {
                        // It's from another context, howerver matching entity has been found. STOP if NOT CROSSCONTEXT
                        throw new InvalidOperationException(
                            @"Provided Entity originates from another Context, 
                            and cross context updates have been disabled.");
                    }

                    break;
                case EntityState.Unchanged:
                    if (!this.AutoDetectChangesEnabled)
                    {
                        // Force change of Entity State to Modified
                        entry.State = EntityState.Modified;
                    }

                    break;
                case EntityState.Deleted:
                    // An entity has already been deleted
                    throw new InvalidOperationException(
                        "Provided Entity has already been deleted");
                default:
                    break;
            }
        }

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
        /// <CallSequence>
        /// nameof
        /// Entry
        /// primaryKeyFunc
        /// Find
        /// DefaultPrimaryKeyFunc
        /// Invoke
        /// </CallSequence>
        /// <Exceptions>
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void Delete<T, TKey>(T entity, Func<T, TKey> primaryKeyFunc = null) where T : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.Context.Entry<T>(entity);  // Get a DbEntry object

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached; // Object doesn't exist in database, so leave it as 'Detached'
                    break;
                case EntityState.Detached:
                    T databaseEntity = null;            // A mathcing object from the current context.

                    if (primaryKeyFunc != null)
                    {
                        // Value of primary key
                        var primaryKey = primaryKeyFunc(entity);
                        if (primaryKey != null)
                        {
                            databaseEntity = this.Find<T, TKey>(primaryKey);
                        }
                    }
                    else
                    {
                        // Value of primary key
                        var primaryKey = this.DefaultPrimaryKeyFunc<T>().Invoke(entity);
                        if (primaryKey != null)
                        {
                            databaseEntity = this.Find<T, long>(primaryKey.Value);
                        }
                    }

                    if (databaseEntity != null)
                    {
                        entry = this.Context.Entry<T>(databaseEntity);
                        entry.State = EntityState.Deleted;
                    }
                    else
                    {
                        throw new InvalidOperationException("You are trying to delete non-existing entity.");
                    }

                    break;
                case EntityState.Deleted:
                    throw new InvalidOperationException("You can not delete an object that is already deleted.");
                default:
                    entry.State = EntityState.Deleted;
                    break;
            }
        }

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
        /// <CallSequence>
        /// Find
        /// Set
        /// Remove
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void Delete<T, TKey>(TKey primaryKey) where T : class, new()
        {
            // Find the entity in the current context
            var entity = this.Find<T, TKey>(primaryKey);

            // Remove it from the dataset
            this.Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Saves changes to current context.
        /// </summary>
        /// <returns>
        /// Number of affected entities.
        /// </returns>
        /// <CallSequence>
        /// SaveChanges
        /// </CallSequence>
        /// <Exceptions>
        /// CustomException
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public int Save()
        {
            int retVal = 0;     // Value to be returned

            try
            {
                // Save changes to context
                retVal = this.Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException databaseCcEx)
            {
                throw new CustomException(
                    "Save failed becouse of optimistic concurrency check failed",
                    databaseCcEx);
            }
            catch (Exception ex)
            {
                throw new CustomException(
                    "Save failed",
                    ex);
            }

            return retVal;
        }

        /// <summary>
        /// Disposes the current context.
        /// </summary>
        /// <CallSequence>
        /// Dispose
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        public void Dispose()
        {
            if (this.myContext != null)
            {
                this.myContext.Dispose();
            }

            this.myContext = null;
        }

        /// <summary>
        /// Sets context configuration properties.
        /// </summary>
        /// <param name="lazyLoadingEnabled">
        /// Flag that indicates whether Lazy Loading is active.
        /// </param>
        /// <param name="autoDetectChangesEnabled">
        /// Flag that indicates whether Automatic detection of changes is active.
        /// </param>
        /// <param name="proxyCreationEnabled">
        /// Flag that indicates whether Creation of Proxies is active.
        /// </param>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        private void SetConfig(bool lazyLoadingEnabled, bool autoDetectChangesEnabled, bool proxyCreationEnabled)
        {
            this.LazyLoadingEnabled = lazyLoadingEnabled;
            this.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            this.ProxyCreationEnabled = proxyCreationEnabled;
        }

        /// <summary>
        /// A Default primary key function.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the object.
        /// </typeparam>
        /// <returns>
        /// Value of primary key for the given entity.
        /// </returns>
        /// <CallSequence>
        /// GetProperty
        /// GetValue
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        private Func<T, int?> DefaultPrimaryKeyFunc<T>() where T : class
        {
            Func<T, int?> retVal = null;             // Value to be returned.
            var entityType = typeof(T);             // Get the type from generic parameter

            // Property info of the ID property
            var propInfoId = entityType.GetProperty(
                                            "Id",
                                            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfoId != null
                && (propInfoId.PropertyType == typeof(short)
                    || propInfoId.PropertyType == typeof(int)
                    || propInfoId.PropertyType == typeof(long)))
            {
                retVal = (T entity) => (int)propInfoId.GetValue(entity) == 0 ? null : (int?)propInfoId.GetValue(entity);
            }

            return retVal;
        }

        /// <summary>
        /// Attaches the entity form another context to current context.
        /// (A matching Entity from current context will be detached, and re-attached).
        /// This is required in order to enforce optimistic concurrency for entities that already exist in context.
        /// </summary>
        /// <param name="actualEntity">
        /// An entity to be attached.
        /// </param>
        /// <param name="databaseEntity">
        /// A matching entity from current context.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to be attached.
        /// </typeparam>
        /// <CallSequence>
        /// Entry
        /// SetValues
        /// Set
        /// Attach
        /// </CallSequence>
        /// <Revisions>
        /// Initial Revision - 12/15/2016 04:37 PM - ivan.cojbasic #$
        /// </Revisions>
        private void AttachExistingObjectToContext<T>(T actualEntity, T databaseEntity) where T : class, new()
        {
            // We shall rewrite values on entity from current context
            // This is because we want SetValues method properly to update EntityState value
            var entry = this.Context.Entry<T>(databaseEntity);
            entry.CurrentValues.SetValues(actualEntity);

            // This new state will be of use for the entity we shall attach
            var newState = entry.State;

            // Remove dbEntity from Context
            entry.State = EntityState.Detached;

            // And attach new entity instead of it
            this.Context.Set<T>().Attach(actualEntity);
            entry = this.Context.Entry<T>(actualEntity);

            // Restore state we already resolved
            entry.State = newState;
        }
    }
}
