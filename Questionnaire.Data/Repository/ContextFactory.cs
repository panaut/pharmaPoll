using System.Data.Entity;

namespace Questionnaire.Data.Repository
{
        /// <summary>
    /// The Context Factory.
    /// Generates AD HOC Database Context for project specified by ProjectName property.
    /// </summary>
    /// <Revisions>
    /// Initial Revision - 12/16/2016 04:24 PM - ivan.cojbasic #$
    /// </Revisions>
    public class ContextFactory : IContextFactory
    {
        /// <summary>
        /// Creates new instance of Database Context.
        /// </summary>
        /// <returns>Database Context for project specified by ProjectName property.</returns>
        /// <Exceptions>
        /// Out Of Memory Exception
        /// </Exceptions>
        /// <Revisions>
        /// Initial Revision - 12/16/2016 04:28 PM - ivan.cojbasic #$
        /// </Revisions>
        public DbContext GetNewContext()
        {
            return new SurveyModelContext(true, true, true);
        }
    }
}
