namespace Questionnaire.Data.Repository
{
    using System.Data.Entity;

    /// <summary>
    /// The Context Factory.
    /// Generates AD HOC Database Context for project specified by ProjectName property.
    /// </summary>
    /// <Revisions>
    /// Initial Revision - 12/16/2016 04:24 PM - ivan.cojbasic #$
    /// </Revisions>
    public interface IContextFactory
    {
        /// <summary>
        /// Creates new instance of Database Context.
        /// </summary>
        /// <returns>
        /// Database Context for project specified by ProjectName property.
        /// </returns>
        DbContext GetNewContext();
    }
}
